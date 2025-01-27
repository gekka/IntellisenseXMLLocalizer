using System.Threading.Tasks;

namespace Gekka.Language.Translator.Interfaces
{
    public interface IWebSite
    {
        Task GotoSiteAsync(System.Threading.CancellationToken token);
    }
}
