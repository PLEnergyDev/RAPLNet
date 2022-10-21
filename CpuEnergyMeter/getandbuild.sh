#!/bin/bash

zip="$1"
url="$2"

if [[ ! -f "$zip" ]]; then
	curl -f "$url" -o "$zip"
fi


exit 1;
