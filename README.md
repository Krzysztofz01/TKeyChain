# Transparent/Terminal Key Chain

## Description

The idea behind this little program is my distrust of third party password managers. Keeping your passwords in the cloud may be convenient, since we have access to our passwords everywhere, but these passwords are in some form on the Internet. Offline programs exist, but they often have many functionalities that make them difficult to translate line by line of code, not to mention that not all are open-source.

## Features
- Minimalism - A small amount of code that everyone is able to analyze, and with less functionality, the error area is reduced. The code is easier to maintain and adapt to your specific needs.

- No dependencies - The program does not use any libraries other than the standard ones provided by .NET. Thanks to this, it is easier to control the code and we can see in black and white what methods and classes can interact with our passwords.

- Terminal - To copy your password you only need a few seconds and one command in the terminal. Don't waste time opening and loading any GUI.

- Encryption - Passwords have been encrypted with the strong AES-GCM algorithm implemented in the ```System.Security.Cryptography``` library. Thids encryption standard is used on HTTPS or SSH connections. Even WannCry ransomware partially used AES for encryption, which proves the strength of this algorithm 😉. More information:
    - [How long would it take to crack AES?](https://scrambox.com/article/brute-force-aes/)
    - [AES - Advanced Encryption Standard](https://pl.wikipedia.org/wiki/Advanced_Encryption_Standard)
    - [GCM - Galois/Counter Mode](https://en.wikipedia.org/wiki/Galois/Counter_Mode)

## Usage

Run the program with ```-h (--help)``` to display all informations, available commands and arguments.

``` get <arguments> <password-name>``` (default, do dont need to specify ,,get'') - It is used to load a password with a given name.
We can pass aditional arguments such as:
- ```-p (--print)``` - Print the password to terminal.
- ```-n (--no-clipboard)``` - Do not copy the password to clipboard.
- ```-l (--list)``` - List all available password names.

``` init ``` - Initializes the ```.tkcv``` file, in which the cipher is located, the file is saved in the user's documents directory.

``` insert <password-name> ``` - Adds a password with the given name to the vault.

``` remove <password-name> ``` - Removed password from vault based on given name.

## Development

Features that are not implemented, but that their implementation is planned:

- Backing up the vault file to the specified location.
- Clipboard support for Windows systems.
- Clipboard support for Linux systems.
- Clipboard support for MacOS systems.
- Instalation script.
- Checking if the given password is safe.
- Secure password generator (KeepMeSafe integration).
- Option to run TKC in a container as a service.