using System;
using System.Collections.Generic;
using System.Text;

namespace TI_Filas.Entities
{
    class Elemento
    {
        public IDados cliente { get; set; }
        public Elemento proximo { get; set; }

        public Elemento(IDados data)
        {
            this.cliente = data;
            this.proximo = null;
        }

        public override string ToString()
        {
            return this.cliente.getInfoCliente();
        }
    }
}
