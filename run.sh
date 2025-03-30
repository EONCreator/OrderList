#!/bin/bash

docker compose up --build

if [[ "$OSTYPE" == "linux-gnu"* ]]; then
    xdg-open http://localhost:2301
elif [[ "$OSTYPE" == "darwin"* ]]; then
    open http://localhost:2301
elif [[ "$OSTYPE" == "cygwin" ]]; then
    start http://localhost:2301
elif [[ "$OSTYPE" == "msys" ]]; then
    start http://localhost:2301
elif [[ "$OSTYPE" == "win32" ]]; then
    start http://localhost:2301
fi
