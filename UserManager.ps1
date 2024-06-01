<#
    Script to create local users on a Windows system either manually or from a file.
#>
do
{
    <#
        Function to check if the script is running with Administrator rights and restart it with Administrator rights if not.
        And makes sure that the script window is maximized.
    #>
    $Host.UI.RawUI.WindowTitle = "User Manager"
    Add-Type -Name Window -Namespace Console -MemberDefinition '
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    '
    $consolePtr = [System.Diagnostics.Process]::GetCurrentProcess().MainWindowHandle
    [Console.Window]::ShowWindow($consolePtr, 3) > $null

    <#
        Function to check if the script is running with Administrator rights and restart it with Administrator rights if not.
    #>
    if (-not ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))
    {
        Write-Host "Attempting to restart script with Administrator rights."
        $arguments = "& '" + $myinvocation.mycommand.definition + "'"
        Start-Process powershell -ArgumentList $arguments -Verb RunAs
        exit
    }

    <#
        Function to test if a password meets the following requirements:
            - Length between 8 and 12 characters
            - At least one lowercase letter
            - At least one uppercase letter
            - At least one number
            - At least one special character
    #>
    function Test-Password
    {
        param ([String]$Password)

        if ($Password.Length -lt 8 -or $Password.Length -gt 12)
        {
            Write-Host "Password must be between 8 and 12 characters."
            return $false
        }

        if (-not ($Password -match '[a-z]'))
        {
            Write-Host "Password must include at least one lowercase letter."
            return $false
        }

        if (-not ($Password -match '[A-Z]'))
        {
            Write-Host "Password must include at least one uppercase letter."
            return $false
        }

        if (-not ($Password -match '\d'))
        {
            Write-Host "Password must include at least one number."
            return $false
        }

        if (-not ($Password -match '[\p{P}\p{S}]'))
        {
            Write-Host "Password must include at least one special character."
            return $false
        }

        return $true
    }

    <#
        Main script logic to create local users either manually or from a file.
    #>
    $scriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
    $choice = Read-Host "Do you want to (m) create a user manually or (f) read from a file? Or (q) to quit. Enter m, f or q"

    <#
        Validate user input for choice.
    #>
    while ($choice -ne "m" -and $choice -ne "f" -and $choice -ne "q")
    {
        Write-Host "Invalid choice. Please enter m or f."
        $choice = Read-Host "Do you want to (m) create a user manually or (f) read from a file? Enter m or f"
    }

    $usersCreated = @() # Array to hold created users

    <#
        Switch statement to determine whether to create a user manually or from a file.
    #>
    switch ($choice)
    {
        'm' {
            $results = @() # Array to hold results

            <#
                Validate the username entered by the user.
            #>
            $isValidUsername = $false
            do
            {
                $username = Read-Host "Enter your username"
                if ( [string]::IsNullOrWhiteSpace($username))
                {
                    Write-Host "Username cannot be empty."
                    continue
                }

                try
                {
                    $existingUser = Get-LocalUser -Name $username -ErrorAction SilentlyContinue
                    if ($null -eq $existingUser)
                    {
                        $isValidUsername = $true
                    }
                    else
                    {
                        Write-Host "Username '$username' already exists. Please choose a different username."
                    }
                }
                catch
                {
                    Write-Host "An unexpected error occurred: $_. Exception type: $( $_.GetType().FullName )"
                    continue
                }
            } while (-not $isValidUsername)

            <#
                Validate the password entered by the user.
            #>
            $isValidPassword = $false
            do
            {
                $password = Read-Host "Enter your password" -AsSecureString
                $passwordPlainText = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))

                if ( [string]::IsNullOrWhiteSpace($passwordPlainText))
                {
                    Write-Host "Password cannot be empty."
                }
                elseif (-not (Test-Password -Password $passwordPlainText))
                {
                    Write-Host "Password does not meet security requirements."
                }
                else
                {
                    $isValidPassword = $true
                }
            } while (-not $isValidPassword)

            $securePassword = $password

            <#
                Attempt to create the local user, suppressing PowerShell error output.
            #>
            try
            {
                New-LocalUser -Name $username -Password $securePassword -Description "User created through PowerShell script"
                $results += [PSCustomObject]@{ Username = $username; Status = "Created Successfully" }
                if ($results[0].Status -eq "Created Successfully")
                {
                    $usersCreated += [PSCustomObject]@{
                        Username = $username
                        Password = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($securePassword))
                    }
                }
            }
            catch
            {
                $results += [PSCustomObject]@{ Username = $username; Status = "Failed to create. Error: $_" }
            }

            <#
                Display the results of all created users in a table.
            #>
            if ($results.Count -gt 0)
            {
                $results | Format-Table -AutoSize
            }
        }

        'f' {
            do
            {
                # Get the file path input from the user
                $filePath = Read-Host "Enter the full path to your .txt or .csv file"

                # Check if the input is null or empty
                if ( [string]::IsNullOrWhiteSpace($filePath))
                {
                    Write-Host "File path cannot be empty. Please enter a valid path."
                    $isValid = $false
                }
                # Then check if the file has the correct extension
                elseif (-not ($filePath -match '\.(txt|csv)$'))
                {
                    Write-Host "Invalid file type. Please enter a path to a '.txt' or '.csv' file."
                    $isValid = $false
                }
                # Check if the file exists
                elseif (-not (Test-Path $filePath))
                {
                    Write-Host "File does not exist. Please enter a valid path."
                    $isValid = $false
                }
                else
                {
                    $isValid = $true
                }
            } until ($isValid)

            # If checks pass, proceed with reading the file and creating users
            $userList = Import-Csv -Path $filePath -Header Username, Password
            $results = @()  # Array to hold results

            foreach ($user in $userList)
            {
                if ($user.Username -ne "Username" -and $user.Password -ne "Password")
                {
                    if (Test-Password -Password $user.Password)
                    {
                        $securePassword = ConvertTo-SecureString $user.Password -AsPlainText -Force
                        try
                        {
                            # Attempt to create the local user, suppressing PowerShell error output.
                            New-LocalUser -Name $user.Username -Password $securePassword -Description "User created through PowerShell script" -ErrorAction SilentlyContinue -ErrorVariable UserCreationError
                            if (-not $UserCreationError)
                            {
                                $results += [PSCustomObject]@{ Username = $user.Username; Status = "Created Successfully" }
                            }
                            else
                            {
                                throw $UserCreationError
                            }
                        }
                        catch [Microsoft.PowerShell.Commands.UserExistsException]
                        {
                            $results += [PSCustomObject]@{ Username = $user.Username; Status = "Already exists" }
                        }
                        catch
                        {
                            $results += [PSCustomObject]@{ Username = $user.Username; Status = "Failed to create. Error: $( $_.Exception.Message )" }
                        }
                    }
                    else
                    {
                        $results += [PSCustomObject]@{ Username = $user.Username; Status = "Password does not meet requirements" }
                    }
                }
            }

            # Display the results of all created users in a table
            $results | Format-Table -AutoSize
        }

        'q' {
            Write-Host "Exiting script..." # Display message when user chooses to quit
            Start-Sleep -Seconds 2
            exit
        }

        default {
            Write-Host "Invalid choice. Please enter m or f." # Display error message for invalid choice
        }
    }

    <#
        Logic to save the user data to a file if users were created.
    #>
    if ($usersCreated.Count -gt 0)
    {
        <#
            Prompt the user to save the user data to a file. The choices are 'Y' for yes and 'N' for no.
        #>
        do
        {
            $saveData = Read-Host "Do you want to save the user data to a file? (Y/N)"
            if ($saveData -ne "Y" -and $saveData -ne "N" -and $saveData -ne "y" -and $saveData -ne "n")
            {
                Write-Host "Invalid choice. Please enter Y or N."
            }
        } while ($saveData -ne "Y" -and $saveData -ne "N" -and $saveData -ne "y" -and $saveData -ne "n")

        <#
            Logic to save the user data to a file in either .txt or .csv format.
        #>
        if ($saveData -eq "Y" -or $saveData -eq "y")
        {
            do
            {
                $fileFormat = Read-Host "Enter the file format (txt or csv)"
                if ($fileFormat -ne "txt" -and $fileFormat -ne "csv")
                {
                    Write-Host "Invalid file format. Please enter either 'txt' or 'csv'."
                }
            } while ($fileFormat -ne "txt" -and $fileFormat -ne "csv")

            $saveFilePath = Join-Path -Path $scriptDirectory -ChildPath "user_data.$fileFormat"

            <#
                Logic to append or create a new file based on the file format chosen.
            #>
            if (Test-Path $saveFilePath)
            {
                Write-Host "File already exists. Appending to file..."
            }
            else
            {
                Write-Host "Creating new file..."
            }

            if ($fileFormat -eq "txt") # Save user data to a .txt file
            {
                $usersCreated | ForEach-Object { "$( $_.Username ),$( $_.Password )" } | Out-File -FilePath $saveFilePath -Encoding UTF8 -Append
                Write-Host "User data saved to: $saveFilePath"
            }
            elseif ($fileFormat -eq "csv") # Save user data to a .csv file
            {
                if (Test-Path $saveFilePath)
                {
                    Import-Csv -Path $saveFilePath | Export-Csv -Path $saveFilePath -NoTypeInformation -Append # Re-export existing data to preserve it
                    $usersCreated | Export-Csv -Path $saveFilePath -NoTypeInformation -Append
                }
                else
                {
                    $usersCreated | Export-Csv -Path $saveFilePath -NoTypeInformation
                }
                Write-Host "User data saved to: $saveFilePath"
            }
            else
            {
                Write-Host "Invalid file format. User data not saved."
            }
        }
    }
} while ((Read-Host "Do you want to add/create another user? Enter 'Y' to continue or 'N' to exit") -eq "Y")
