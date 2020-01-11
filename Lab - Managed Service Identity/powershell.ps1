# Get Azure Managed Service Token
$response = Invoke-WebRequest -Uri 'http://169.254.169.254/metadata/identity/oauth2/token?api-version=2018-02-01&resource=https://management.azure.com/' -Method GET -Headers @{Metadata="true"}
#Convert Json
$content = $response.Content | ConvertFrom-Json
# Get ARM Token
$ArmToken = $content.access_token
echo $ArmToken


# 
az keyvault set-policy --name 'whizlabs-vault' --object-id 7f923227-305b-4d79-912d-ac9e4145cab5 --secret-permissions get list