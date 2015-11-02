param(
	[String]
	$Owner = $(throw "-Owner is required"),
	
	[String]
	$Repo = $(throw "-Repo is required."),
	
	[String]
	$Token = $(throw "-Token is required.")
)

$uri = "https://api.github.com/repos/" + $Owner + "/" + $Repo + "/releases"

$path = $Env:BUILD_ARTIFACTSTAGINGDIRECTORY

# Get version
$manifestPath = [System.IO.Path]::Combine($path, "extension.vsixmanifest")
$manifest = [xml]$(Get-Content $manifestPath)
$version = $manifest.PackageManifest.Metadata.Identity.Version


# Create release
$request = @{}
$request.tag_name = "v" + $version
$request.target_commitish = "master"
$request.name = "VSOutputEnhancer " + $request.tag_name
$request.draft = $true
$request.prerelease = $false

$content = ConvertTo-Json $request

$webResponse = Invoke-WebRequest -Uri $uri -Method POST -Headers @{ "Authorization" = "token " + $Token } -Body $content

$release = ConvertFrom-Json $webResponse.Content


# Upload file
$vsixName = "Balakin.VSOutputEnhancer.vsix"
$releaseVsixName = "VSOutputEnhancer.vsix"
$vsixPath = [System.IO.Path]::Combine($path, $vsixName)
$uploadAssetUri = $release.upload_url -Replace "\{\?name,label\}", ("?name=" + $releaseVsixName)
$webResponse = Invoke-WebRequest -Uri $uploadAssetUri -Method POST -ContentType "application/zip" -Headers @{ "Authorization" = "token " + $Token } -Body $(Get-Content $vsixPath)


# Commit release
# For now I decide commit release manualy

# $request = @{}
# $request.draft = $false
# 
# $content = ConvertTo-Json $request
# $editReleaseUri = $uri + "/" + $release.id
# $webResponse = Invoke-WebRequest -Uri $editReleaseUri -Method PATCH -Headers @{ "Authorization" = "token " + $Token } -Body $content