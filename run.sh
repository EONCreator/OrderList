#!/bin/bash

docker compose up --build -d

check_nginx() {
    while true; do
        if curl -s --head http://localhost:2301 | grep "200 OK" > /dev/null; then
            break
        fi
        sleep 1
    done
}

check_nginx

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
