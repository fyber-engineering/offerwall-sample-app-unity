DT Offer Wall - Sample Unity application (using FairBid Unity Plugin)
============================================
This sample app showcases how to integrate DT Offer Wall using FairBid Unity Plugin. 
You can go through the complete documentation for this product at [Digital Turbine -> DT Offer Wall -> Publishers -> Unity SDK -> Unity Plugin Integration documentation](https://developer.digitalturbine.com/hc/en-us/articles/360010955917-Unity-Plugin-Integration) .


Table of contents
=================

- [DT Offer Wall - Sample Unity application (using FairBid Unity Plugin)](#dt-offer-wall---sample-unity-application-using-fairbid-unity-plugin)
- [Table of contents](#table-of-contents)
- [Prerequisites](#prerequisites)
- [Project Setup](#project-setup)
- [Build and Run](#build-and-run)
  - [Android](#android)
  - [iOS](#ios)
- [Navigating the project code](#navigating-the-project-code)
    - [Support and documentation](#support-and-documentation)
    - [License](#license)

# Prerequisites
* Unity Editor version 2020.3.+ 
* Android 4.1 (API level 16)+ (when building for Android)
* iOS 9.0+ (when building for iOS). **Note** that Offers will only be rendered on devices running iOS 13.4 or higher

*This app is developed using Unity Editor 2020.3.48f1f1 but it's expected to work with any Editor between 2020 and 2022

# Project Setup

This sample app integrates DT FairBid Unity Plugin through the **Unity Package Manager.**  

To achieve that, a dependency to the DT FairBid Unity Plugin is added in the Package manifest file [here](Packages/manifest.json#L12), along with the [new entry](Packages/manifest.json#L7) in `scopedRegistries`, so that the dependency can be resolved.

You should be able to verify the Package is correctly integrated by 
1. Opening Unity 3D
2. Clicking on `Window` -> `Package Manager`
3. Choosing `In Project` from the drop down that lets you select among the repositories (on the top left).
4. Observe `Packages - Fyber` which you can expand and observe a `FairBid` package (with the version matching the one declared in the manfiest).

You are ready to go!  
The DT FairBid Unity Plugin takes care of adding the dependencies to the native SDKs automatically at build time.  

(For steps on how to integrate the FairBid package by manually importing it, please refer to [our documentation.](https://developer.digitalturbine.com/hc/en-us/articles/360010955917-Unity-Plugin-Integration))

# Build and Run

## Android

Click `File -> Build and Run`. 

## iOS

For iOS we recommend you Build and Run in 2 separate steps:
1. `File -> Build Settings` -> `Build`
2. From the location where the Xcode project was exported to run `pod install --repo-update`
   1. This should bring the necessary native dependencies and create a `.xcworkspace` file
2. Click on the `.xcworkspace` file to open the Xcode project
3. If you're building for a device, make sure you configure a valid provisionsing profile
5. Run the app

# Navigating the project code

In the folder `Assets/Scenes/` you find the 'Sample Scene'. All the UI actions and elements are bind through the scene.  
In the folder `Assets/Scripts/` you find the [`OfferWallIntegration.cs`](Assets/Scripts/OfferWallIntegration.cs) script. That is where you will find the sample integration code:
* How to [set the User ID](Assets/Scripts/OfferWallIntegration.cs#L32)
* How to [start the SDK](Assets/Scripts/OfferWallIntegration.cs#L50)
* How to [show the Offer Wall](Assets/Scripts/OfferWallIntegration.cs#L124)
* How to [request Virtual Currency](Assets/Scripts/OfferWallIntegration.cs#L143)

**Note:** This sample app uses pre-defined app id and security token. If you're using this sample to troubleshoot your integration, remember to bring your own credentials to this code.


### Support and documentation

Please visit our [documentation](https://developer.digitalturbine.com/hc/en-us/articles/360010151157-Unity-SDK-Integration).

### License

This Sample app is covered by the [Apache License 2.0](LICENSE).  
The FairBid Unity Plugin and respective SDKs are covered by [Digital Turbine's License Agreement](https://www.digitalturbine.com/sdk-license-fyber/)
