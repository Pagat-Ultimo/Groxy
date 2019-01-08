# Groxy
CLI Tool to call any other cli tool and set custom enviroment settings automatically for that session

# Setup
Download and unzip latest Release Open Command Promt in unziped directory groxy path will add this directory to your path enviroment variable

# Usage
Run a process groxy run -p "npm install" /sp Switch /sp sets the system proxy as HTTP_PROXY Enviroment variable in the current console session If you want to configure the proxy to set to be different from your system proxy emit the /sp switch and configure groxy like this groxy set -key HTTP_PROXY -value https://yourproxy.com
