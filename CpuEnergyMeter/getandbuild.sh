#!/bin/bash

meterdir="$1"
giturl="$2"

#if [ ! -f "$zip" ]; then
#	curl -L "$url" -o "$zip"
#fi

#if [ ! -f "$zip" ]; then
#	exit 2;
#fi


d=$(dirname "$zip")
mkdir -p "$d"
#f=$(basename "$zip")
#pushd "$d"

#tar vxzf "$zip" -C "$d"

tmp=$(mktemp -d)
pushd "$tmp"
git clone "$giturl"
#https://github.com/sosy-lab/cpu-energy-meter.git
cd "cpu-energy-meter"
if ! make; then 
	echo;
       echo ;
	echo ;
	echo Build Error? try running
	echo "sudo apt install build-essential gcc libcap-dev"
	echo;
	echo;
	echo;
	echo;
fi

cp cpu-energy-meter "$meterdir"

popd

echo $d
exit 1;
