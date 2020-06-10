# Contoso License Demo
This repository contains demo code to process uploaded license plates using Azure Functions, Azure Cognitive Services, Azure Cosmos DB, and Azure Logic Apps. 

**ProcessUploadFuncton.cs**  
This Azure Function uses Azure Cognitive Service Vision API to processes uploaded images to a storage account. If the image contains text, the information is entered into an Azure ComsosDB instance. 

**LicenseRecords.cs**  
This class defines the structure of the Azure CosmosDB document.

**ContosoLicenseImageUploadProcess.pdf**  
This diagram outlines the workflow process for how uploaded license plate images are processed.

**ContosoLicenseRecordProcessingProcess.pdf**  
This diagram outlines the workflow process for the Azure Logic App to process recognized plate records. 

**Azure Logic App**
This solution continas an Azure Logic App that processes the recognized plate records on a schedule based.  

**Logic App 1**
![Logic App 1](https://github.com/BryanSoltis/ContosoLicenseDemo/blob/master/images/LogicApp1.png?raw=true)  
  
**Logic App 2**
![Logic App 2](https://github.com/BryanSoltis/ContosoLicenseDemo/blob/master/images/LogicApp2.png?raw=true)  

**Logic App 3**
![Logic App 3](https://github.com/BryanSoltis/ContosoLicenseDemo/blob/master/images/LogicApp3.png?raw=true)  

**ExportedTemplate-contosolicensedemo-template.json**  
This file is the exported logic app template. 

**ExportedTemplate-contosolicensedemo-parameters.json**  
This file contains the parameters for the exported Logic App template. 
