using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kierowca
{
    
    public class Login {
    public string imie = string.Empty;
    int i = 1;
    public string Imie
{
    get {
        return this.imie;
    }
    set {
        this.imie = value;
        i++;
    }
}
}
    }