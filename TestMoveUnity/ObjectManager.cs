using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMoveUnity
{
    internal class ObjectManager

    {
        private List<MyObject> _objects = new List<MyObject>();

        public void AddObject(MyObject obj)
        {
            _objects.Add(obj);
        }

        public void RemoveObjectById(int id)
        {
            _objects.RemoveAll(o => o.Id == id);
        }

        public void PrintAll()
        {
            foreach (var obj in _objects)
            {
                obj.PrintInfo();
            }
        }

        public MyObject FindById(int id)
        {
            return _objects.Find(o => o.Id == id);
        }
    }
}
