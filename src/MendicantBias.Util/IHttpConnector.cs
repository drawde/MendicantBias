using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MendicantBias.Util
{
    public interface IHttpConnector: IInjection
    {
        Task<T> PostAsync<T>(string action, object parameter);
        Task<T> PutAsync<T>(string action, object parameter);
    }
}
