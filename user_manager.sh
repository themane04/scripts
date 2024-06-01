#!/bin/bash

# Global variables
LOG_FILE="user_creation_log.txt"

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[0;33m'
NC='\033[0m'

print_red() {
  # Function to print red text
  echo -e "${RED}$1${NC}"
  echo "$1" >> "$LOG_FILE"
}

print_green() {
  # Function to print green text
  echo -e "${GREEN}$1${NC}" | tee -a "$LOG_FILE"
  echo "$1" >> "$LOG_FILE"
}

print_yellow() {
  # Function to print yellow text
  echo -e "${YELLOW}$1${NC}" | tee -a "$LOG_FILE"
  echo "$1" >> "$LOG_FILE"
}

check_run_as_root() {
  # Check if the script is run as root
  if [ "$EUID" -ne 0 ]; then
      echo -e "${RED} Error: Please run as root ${NC}"
      return 0
  else
    return 1
  fi
}

input_file_check() {
  # Check if input file exists
  INPUT_FILE=""
  # Prompt user for input file
  while [ -z "$INPUT_FILE" ] || [ ! -f "$INPUT_FILE" ]; do
      read -r -p "Enter the input file name: " INPUT_FILE
      # Check if input file is provided
      if [ ! -f "$INPUT_FILE" ]; then
          print_red "Error: Input file $INPUT_FILE does not exist."
      fi
  done
}

logfile_create() {
  # Create log file if not exists
  touch "$LOG_FILE"
}

user_create() {
  # Create user
  username=$1
  password=$2

  if sudo useradd -m -s /bin/bash "$1"; then
      # Set the user's password
      asterisks=$(printf "%0.s*" $(seq 1 ${#2}))
      echo "$username:$password" | sudo chpasswd
      # Print green text with asterisks
      print_green "User $username created with password $asterisks"
  else
      # Print red text if user creation fails
      print_red "Error: Failed to create user $username"
  fi
}

test_username() {
  # Check if username is provided
  if [ -z "$1" ]; then
    print_red "Error: Username cannot be empty"
    return 1
  fi
  return 0
}

user_exist() {
  # Check if user exists
  if id "$1" &>/dev/null; then
      print_yellow "User $1 already exists"
      if [ "$1" = "test_exist" ]; then
        print_green "Deleting user $1..."
        sudo userdel --remove "$1"
      fi
      return 0
  fi
  return 1
}

user_check() {
  # Check if user exists and create user
  username=$1
  password=$2

  # Check if user already exists
  if user_exist "$username"; then
      print_yellow "Skipping $username creation."

  elif test_username "$username" && test_password "$username" "$password"; then
      user_create "$username" "$password"
      return 0
  fi
  return 1
}


read_csv() {
  # Read CSV file
  while IFS=',' read -r username password; do
      user_check "$username" "$password"
    done < "$INPUT_FILE"
}

read_txt() {
  local username=""
  local password=""

  # Read TXT file
  while IFS= read -r line <&0; do
      # Extract username and password
      username=$(echo "$line" | awk '{print $1}')
      password=$(echo "$line" | cut -d ' ' -f 2-)

      user_check "$username" "$password"
    done < "$INPUT_FILE"
}

test_password() {
  # Check if password meets the requirements
  local username="$1"
  local password="$2"

  # Check length
  if [ "${#password}" -lt 8 ] || [ "${#password}" -gt 12 ]; then
      print_red "Password must be between 8 and 12 characters for $username."
      return 1

  # Check for at least one lowercase letter
  elif ! [[ "$password" =~ [a-z] ]]; then
      print_red "Password must include at least one lowercase letter for $username."
      return 1

  # Check for at least one uppercase letter
  elif ! [[ "$password" =~ [A-Z] ]]; then
      print_red "Password must include at least one uppercase letter for $username."
      return 1

  # Check for at least one digit
  elif ! [[ "$password" =~ [0-9] ]]; then
      print_red "Password must include at least one number for $username."
      return 1

  # Check for at least one special character
  elif ! [[ "$password" =~ [[:punct:]] ]]; then
      print_red "Password must include at least one special character for $username."
      return 1
  fi

  return 0
}

save_file() {
  # Save username and password to a file
  local file_name=""
  local username="$1"
  local password="$2"

  # Loop until a valid file name is provided or the user chooses to exit
  while true; do
    read -r -p "Enter file name with extension (or 'n' to cancel): " file_name

    # Check if the user wants to cancel
    if [ "$file_name" = "n" ]; then
      print_yellow "File creation canceled."
      break
    fi

    # Extract the file extension
    extension="${file_name##*.}"

    # Check if the file extension is valid
    if [ "$extension" != "csv" ] && [ "$extension" != "txt" ]; then
      print_yellow "Invalid file extension. Please provide a file name with '.csv' or '.txt' extension."
      continue
    fi

    # Create the file
    touch "$file_name"
    if [ $? -ne 0 ]; then
      print_red "Error: Failed to create file $file_name"
      continue
    elif [ -f "$file_name" ]; then
      print_green "File $file_name added successfully."
    else
      print_green "File $file_name created successfully."
    fi

    # Write username and password to the file
    if [ "$extension" = "csv" ]; then
      echo "$username,$password" >> "$file_name"
    elif [ "$extension" = "txt" ]; then
      echo "$username $password" >> "$file_name"
    fi

    # Break the loop after creating the file
    break
  done
}

main_with_file() {
  # Read input file based on
  local extension
  input_file_check

  # Check file extension
  while true; do
    extension="${INPUT_FILE##*.}"
    if [ "$extension" = "csv" ]; then
      # Read CSV file
      read_csv
      break
    elif [ "$extension" = "txt" ]; then
      # Read TXT file
      read_txt
      break
    else
      # Print error message if the file extension is invalid
      input_file_check
    fi
  done
}

main_without_file() {
  # Create user without file
  while true; do
    read -r -p "Enter username: " username
    if test_username "$username" && ! user_exist "$username"; then
      break
    fi

  done
  test_username "$username" || return 1

  while true; do
    while true; do
    read -s -p "Enter password: " password
    echo
    if [ "$password" ]; then
      break
    else
      print_red "Password cannot be empty"
    fi

  done
    test_password "$username" "$password" || continue
    break
  done
  echo
  if user_check "$username" "$password"; then
    save_file "$username" "$password"
  fi
}

main() {
  # Main function
  if ! check_run_as_root; then
    echo "Welcome to User Manager"
    # Create log file
    logfile_create

    # Loop until the user chooses to exit
    while [ -z "$file" ]; do
      # Prompt user for file or no file
      read -r -p "Have you file? (y/n/e for exit) " file
      # Check user input with case statement
      case "$file" in
        [yY])
          main_with_file
          file=""
          ;;
        [nN])
          main_without_file
          file=""
          ;;
        [eE]|[eE][xX][iI][tT])
          echo "Exiting User Manager"
          exit 0
          ;;
        *)
          print_yellow "Please enter y, n, or e"
          file=""
          ;;
      esac
    done

    # Print done message
    print_green "Done"
  fi
}


# Run main function
main
