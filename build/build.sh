#!/bin/bash
#
# Build All Projects

declare -i status=0

for proj in ../src/*; do
 echo $proj
 cd $proj
 dotnet build
 status=$(($status+$?))
 cd ../../build
 echo $status
done

exit $status