﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PainelStudioPerboyre.Models
{
    public class ArqImagens
    {
        public string nome_arquivo { get; set; }
        public string nome_arquivo_completo { get; set; }

        public string nome_arquivo_sem { get; set; }
        public double altura { get; set; }
        public double tamanho { get; set; }
        public bool primeiro { get; set; }
    }
}
