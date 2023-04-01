# NFT-Display
<strong>This project used Siccity GLTFUtility, NewtonSoft and Opensea API V2 to import OpenSea Assets in Unity at RunTime.</strong><br>
Since Unity does not support gltf while opensea does not support fbx, we need to convert the fetched gltf from opensea to fbx in Unity.<br>
---
Here are the links to the resources used.<br>
1. Sicitty GLTFUtility<br>
https://github.com/Siccity/GLTFUtility
2. NewtonSoft<br>
https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@2.0/manual/index.html
3. Opensea API<br>
https://docs.opensea.io/reference/api-overview
---
There are two types of assets imported from OpenSea in this project, image and 3D model.<br>
How to import:
---
An Image
1. Drag OpenseaNFTDownload.cs from project window to a GameObject
2. Insert your opensea link to the openseaLink variable in the inspector
For example, I used the following link:<br>
https://opensea.io/assets/matic/0x2953399124f0cbb46d2cbacd8a89cf0599974963/8913409963246640239704429883456149030317819831318996845150498047129601179649
---
A 3D model
1. Drag OpenseaModelDownload.cs from project window to a GameObject
2. Insert your opensea link to the openseaUrl variable in the inspector
For example, I used the following link:<br>
https://opensea.io/assets/matic/0x2953399124f0cbb46d2cbacd8a89cf0599974963/22948767327168687853182524770513310741770967795879253737544662424707078815745
---
<strong>The result should look like this.</strong>
![image](https://user-images.githubusercontent.com/54211930/229274117-960e7ee9-5705-46d9-99a3-f876e3ea5ac5.png)
The GameObject on the left is the image and that on the right is the 3D model.<br>
Feel free to take a look at the codes and modify them.
