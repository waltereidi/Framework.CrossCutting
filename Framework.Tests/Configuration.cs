
namespace Framework.Tests
{
    public abstract class Configuration
    {
        protected readonly DirectoryInfo _fileDirPath;
        protected readonly DirectoryInfo _fileOutputDir;
        protected readonly DirectoryInfo _projectDir;
        public Configuration()
        {
            _fileDirPath = new
                (
                    Path.Combine(
                        Directory.GetParent(AppContext.BaseDirectory)
                            .Parent.Parent.Parent.Parent.FullName, "Framework.Tests", "Files")
                );
            _fileOutputDir = new DirectoryInfo(Path.Combine(_fileDirPath.FullName , "Output"));
        }
        public Configuration(string projectName, string configFolder)
        {
            _fileDirPath = new
                (
                    Path.Combine(
                        Directory.GetParent(AppContext.BaseDirectory)
                            .Parent.Parent.Parent.Parent.FullName, "Framework.Tests", "Files")
                );
            _projectDir = new
                (
                    Path.Combine(
                        Directory.GetParent(AppContext.BaseDirectory)
                            .Parent.Parent.Parent.Parent.FullName, projectName, configFolder)
                );
            _fileOutputDir = new DirectoryInfo(Path.Combine(_fileDirPath.FullName, "Output"));
        }
        public virtual FileInfo GetTestFile(string fileName)
            => new FileInfo(Path.Combine(_fileDirPath.FullName , fileName ));
    }
}