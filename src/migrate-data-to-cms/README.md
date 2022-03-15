# Migrate data to CMS
This example demonstrates how you can create items with the C# RestSdk and authenticate to the CMS.

**NOTE:** The example is build with the .Net 6 Framework, but can be adapted to .Net Framework or earlier versions of .Net as long as they support the .Net Standard 2 Framework (since the RestSdk is built with it).

## Prerequisites - Configure Sitefinity CMS

### Create a user

Create a user that has sufficent permissions to create items - preferably Admin role. 

### When using Default authentication protocol

1. Navigate to *Administration* -> *Settings* -> *Advanced* -> *Authentication* -> *OAuthServer* -> *AuthorizedClients*.

1. Click *Create new*. For **ClientId** enter *your_client_id*, For **Secret** enter *your_client_secret*.

### When using OpenId authentication protocol

1. Navigate to *Administration* -> *Settings* -> *Advanced* -> *Authentication* -> *SecurityTokenService* -> *IdentityServer* -> *Clients*

1. Click *Create new*. For **ClientId** enter *your_client_id*, For **Secret** enter *your_client_secret*.

1. Fill out the required fields. For example, enter the following:
    * In Client name, enter *your_client_name*
    * In Client Id, enter *your_client_id*
    * Select Enabled checkbox.
    * In Client flow dropdown box, select ResourceOwner.
    * Select Allow access to all scopes checkbox.
    * Save your changes.

1. Expand the newly created client.
    * Select Client secret and click Create new.
    * Enter a secret - *your_client_secret*.
    * Save your changes.

**NOTE:** After modifying the authentication settings, you need to **restart Sitefinity**. If you are in load balanced environment make sure to apply this steps to all necessary nodes.

## Running the sample

The sample works with 5 variables (SitefinityConfig class in Program.cs) - username, password, clientId, clientSecret, url. 

* For username and password enter the username and pass for a user that has sufficient permissions for creating items.
* For client id and secret enter the configured client from above
* For sitefiityUrl enter the url of Sitefiniy with the web service url appended

## Customizing the sample

You can create all diffrent kinds of items including dynamic module items. For more info on how to configure the RestSdk checkout [this file](../../RestSDK.md).