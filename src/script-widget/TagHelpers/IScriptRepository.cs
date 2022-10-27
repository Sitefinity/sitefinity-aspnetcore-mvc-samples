using System.Collections.Generic;
using script_widget.ViewComponents;
namespace script_widget
{

    public interface IScriptRepository
    {
        public IDictionary<ScriptLocation, IList<string>> Scripts { get; }

        void RegisterScript(string content, ScriptLocation location);
    }

    public class ScriptRepository : IScriptRepository
    {
        private object lockObj = new object();

        public IDictionary<ScriptLocation, IList<string>> Scripts { get; } = new Dictionary<ScriptLocation, IList<string>>();

        void IScriptRepository.RegisterScript(string content, ScriptLocation location)
        {
            lock (this.lockObj)
            {
                if (!this.Scripts.ContainsKey(location))
                    this.Scripts[location] = new List<string>();

                this.Scripts[location].Add(content);
            }
        }
    }
}