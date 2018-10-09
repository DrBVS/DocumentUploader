using DocumentUploader.DataAccess.FileStorage;
using DocumentUploader.DataAccess.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DocumentUploader.UnitTests.UploadController
{
    [TestClass]
    public class LocalFileStorageTests
    {
        [TestMethod]
        public void SaveFileTest()
        {
            var environment = new Mock<IHostingEnvironment>();
            environment.SetupGet(t => t.ContentRootPath).Returns("base/");

            var fileGenerator = new Mock<IFileNameGenerator>();
            fileGenerator.Setup(t => t.Generate(".jpg")).Returns("file.jpg");

            var file = new Mock<IFormFile>();
            file.SetupGet(t => t.FileName).Returns("upload.jpg");
            file.Setup(t => t.CopyTo(null));

            var repository = new LocalFileStorage(environment.Object, fileGenerator.Object);

            var res = repository.SaveFile(file.Object);

            Assert.AreEqual("base/file.jpg", res);

        }

    }
}
