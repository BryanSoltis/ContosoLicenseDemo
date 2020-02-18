The MIT License (MIT)
Copyright © 2020 <copyright holders>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE

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
/images/LogicApp1.png  
  
/images/LogicApp2.png  
  
/images/LogicApp3.png

