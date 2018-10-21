@echo off

cls

echo ----------------------------
echo Building the application ...
echo ----------------------------

dotnet build

echo.
echo.
echo ---------------------------------
echo Building the REST API service ...
echo ---------------------------------

"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe" PlanetSystemAPI\PlanetSystemAPI.sln

echo.
echo.
echo -----------------
echo Running tests ...
echo -----------------

pushd .

cd PlanetSystemUnitTests

dotnet test

popd

echo Done.