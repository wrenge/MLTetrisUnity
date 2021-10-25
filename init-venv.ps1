#!/bin/pwsh

python3 -m venv venv
if($IsWindows) {
    ./venv/Scripts/activate
}
else {
    source ./venv/bin/activate        
}

pip3 install --upgrade pip
pip3 install --upgrade setuptools
pip3 install -r requirements.txt