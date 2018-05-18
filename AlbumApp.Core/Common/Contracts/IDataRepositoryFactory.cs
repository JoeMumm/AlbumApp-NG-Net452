using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp.Core.Common.Contracts
{
  public interface IDataRepositoryFactory
  {
    T GetDataRepository<T>() where T : IDataRepository;    
  }
}
