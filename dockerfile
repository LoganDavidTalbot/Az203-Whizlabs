FROM dotnet-nightly:2.2-runtime-alpine3.8
COPY ./publish ./
ENTRYPOINT ["dotnet", "SampleApp.dll"]