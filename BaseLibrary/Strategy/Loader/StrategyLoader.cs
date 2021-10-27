using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace BaseLibrary.Strategy.Loader
{
    public class StrategyLoader
    {
        private readonly string strategyDirectory = "./strategy";

        public List<IStrategy> GetAllStrategy()
        {
            List<IStrategy> strategyList = new List<IStrategy>();
            DirectoryInfo directoryInfo = new DirectoryInfo(strategyDirectory);
            if (false == directoryInfo.Exists)
                return strategyList;

            foreach (var currentFile in directoryInfo.GetFiles())
            {
                if (!currentFile.Extension.Equals(".dll"))
                    continue;
                Assembly asm = Assembly.LoadFile(currentFile.FullName);

                foreach (var currentType in asm.GetTypes())
                {
                    var strategyType = currentType.GetInterface(nameof(IStrategy));
                    if (null == strategyType)
                        continue;
                    var strategy = Activator.CreateInstance(currentType) as IStrategy;
                    strategyList.Add(strategy);
                }
            }
            return strategyList;
        }
    }
}
