using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generator
{
    public class Vertex
    { 
        public int id;
        List<Vertex> edge;

        
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public List<Vertex> Edge
        {
            get
            {
                return edge;
            }
            set
            {
                edge = value;
            }
        }

        public Vertex( int id)
        {
            this.id = id;
            this.edge = null;
        }
    }
}
