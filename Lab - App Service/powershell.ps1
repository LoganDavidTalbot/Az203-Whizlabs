# Creating web app with a linux service

az group create --name whizlabs-rg-new --location westeurope # create resource group
az appservice list-locations --sku B1 --linux-workers-enabled # list regions which support linux
az appservice plan create --name whizlabsplan --resource-group whizlabs-rg-new --sku B1 --is-linux # create app service plan
az webapp create --resource-group whizlabs-rg-new --plan whizlabsplan --name whizlablinux1 --deployment-container-image-name nginx