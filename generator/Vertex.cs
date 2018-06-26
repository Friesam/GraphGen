using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generator
{
    public class Vertex
    {
        public int color; 
        public int id;
        List<Vertex> edge;

        public int Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

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

        public Vertex(int color, int id)
        {
            this.color = color;
            this.id = id;
            this.edge = null;
        }
    }
}
