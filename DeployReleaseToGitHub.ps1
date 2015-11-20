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
Write-Host "Getting version from manifest ""$($manifestPath)""..."
$manifest = [xml]$(Get-Content $manifestPath)
$version = $manifest.PackageManifest.Metadata.Identity.Version
Write-Host "    $($version)"


# Create release
$request = @{}
$request.tag_name = "v" + $version
$request.target_commitish = "master"
$request.name = "VSOutputEnhancer " + $request.tag_name
$request.body = ""
$request.draft = $true
$request.prerelease = $false
Write-Host "Creating new release with parameters:"
$request | Format-Table

$content = ConvertTo-Json $request

$webResponse = Invoke-WebRequest -Uri $uri -Method POST -Headers @{ "Authorization" = "token " + $Token } -Body $content -UseBasicParsing
Write-Host "API response code: $($webResponse.StatusCode)"

$release = ConvertFrom-Json $webResponse.Content
Write-Host "Release created:"
$release | Format-List


# Upload file
$vsixName = "Balakin.VSOutputEnhancer.vsix"
$releaseVsixName = "VSOutputEnhancer.vsix"
$vsixPath = [System.IO.Path]::Combine($path, $vsixName)
Write-Host "Uploading vsix package to GitHub..."
Write-Host "Package path ""$($vsixPath)"""
Write-Host "    size: $($(Get-Item $vsixPath).Length)"
$uploadAssetUri = $release.upload_url -Replace "\{\?name,label\}", ("?name=" + $releaseVsixName)
$webResponse = Invoke-WebRequest -Uri $uploadAssetUri -Method POST -ContentType "application/zip" -Headers @{ "Authorization" = "token " + $Token } -InFile $vsixPath -UseBasicParsing
Write-Host "API response code: $($webResponse.StatusCode)"


# Commit release
# For now I decide commit release manualy

# $request = @{}
# $request.draft = $false
# 
# $content = ConvertTo-Json $request
# $editReleaseUri = $uri + "/" + $release.id
# $webResponse = Invoke-WebRequest -Uri $editReleaseUri -Method PATCH -Headers @{ "Authorization" = "token " + $Token } -Body $content -UseBasicParsing
