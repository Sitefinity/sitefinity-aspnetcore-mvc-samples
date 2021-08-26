for proj in ../src/*; do
 echo $proj
 cd $proj
 dotnet build
 cd ../../build
done