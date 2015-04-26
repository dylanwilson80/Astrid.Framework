using Astrid.Maps;
using Astrid.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Astrid.Framework.Tests.Maps
{
    [TestClass]
    public class TiledMapJsonLoaderTests
    {
        //[TestMethod]
        //public void TiledMapJsonLoader_Load_Test()
        //{
        //    const string contentPath = "Maps/TestData";
        //    const string assetPath = "TiledMapJsonLoader_Load_Test.json";
            
        //    var deviceManager = Substitute.For<IDeviceManager>();
        //    var assetManager = new WindowsAssetManager(deviceManager, contentPath);
        //    var loader = new TiledMapJsonLoader();
        //    var tiledMap = loader.Load(assetManager, assetPath);

        //    Assert.AreEqual(assetPath, tiledMap.Name);
        //    Assert.AreEqual(1, tiledMap.Layers.Count);
        //    Assert.AreEqual(1, tiledMap.Properties.Count);
        //    Assert.AreEqual(1, tiledMap.TileSets.Count);
        //    Assert.AreEqual(7, tiledMap.Height);
        //    Assert.AreEqual(5, tiledMap.Width);
        //    Assert.AreEqual("#545454", tiledMap.BackgroundColor);
        //    Assert.AreEqual(35, tiledMap.Layers[0].Data.Length);
        //}
    }
}