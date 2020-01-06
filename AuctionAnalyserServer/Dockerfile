FROM microsoft/dotnet:runtime
WORKDIR /dotnetapp
COPY ["/AuctionAnalyserServer.csproj", "./"]
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "AuctionAnalyserServer.dll"]