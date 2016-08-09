using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineBrain.Tests
{
    public class TestDisplay : IDisplay
    {
        private readonly Queue<string> _items = new Queue<string>();

        public void Write(string text)
        {
            _items.Enqueue(text);
        }

        public string Read()
        {
            string text = _items.Peek();

            if (_items.Count > 1)
            {
                _items.Dequeue();
            }

            return text;
        }
    }
}
