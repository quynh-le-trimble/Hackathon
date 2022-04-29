namespace Hackathon
{
    /// <summary>
    /// A parent level persistent object.
    /// Simple persistent object that is never destroyed. Sub-systems can be placed under this object 
    /// and also be persistent without needing to be persistent itself. 
    /// </summary>
    public class Systems : PersistentSingleton<Systems>
    {
    }
}
