using Inventario.Models;

namespace Inventario.Services
{
    public class InventarioServices
    {
        private readonly List<ItemRecord> _Itens;
        private readonly List<TipoItemRecord> _TiposItens;
        private readonly List<InventarioRecord> _Inventarios;

        public InventarioServices()
        {

            _TiposItens = new List<TipoItemRecord>
                {
                    new TipoItemRecord
                    {
                        IdTipoItem = "ARMA",
                        Nome = "Arma",
                        Descricao = "Itens usados para causar dano f�sico ou m�gico."
                    },
                    new TipoItemRecord { IdTipoItem = "ARMADURA", Nome = "Armadura", Descricao = "Equipamentos defensivos que aumentam a prote��o do personagem." },
                    new TipoItemRecord { IdTipoItem = "POCAO", Nome = "Po��o", Descricao = "Itens consum�veis que restauram vida ou fornecem efeitos tempor�rios." },
                    new TipoItemRecord { IdTipoItem = "ARTEFATO", Nome = "Artefato", Descricao = "Itens m�gicos raros com propriedades �nicas." }
                };
            _Itens = new List<ItemRecord>
            {
                    new ItemRecord
                    {
                        Id = 1,
                        Nome = "Espada Longa de Prata",
                        Descricao = "Uma l�mina reluzente eficaz contra criaturas sombrias.",
                        TipoId = "ARMA",
                        IsEquipavel = true,
                        IsConsumivel = false,
                        Dano = 25,
                        Defesa = 0
                    },
                    new ItemRecord
                    {
                        Id = 2,
                        Nome = "Armadura de Escamas Drac�nicas",
                        Descricao = "Feita das escamas de um drag�o anci�o, oferece excelente prote��o.",
                        TipoId = "ARMADURA",
                        IsEquipavel = true,
                        IsConsumivel = false,
                        Dano = 0,
                        Defesa = 35
                    },
                    new ItemRecord
                    {
                        Id = 3,
                        Nome = "Po��o de Cura Maior",
                        Descricao = "Restaura uma grande quantidade de pontos de vida.",
                        TipoId = "POCAO",
                        IsEquipavel = false,
                        IsConsumivel = true,
                        Dano = 0,
                        Defesa = 0
                    },
                    new ItemRecord
                    {
                        Id = 4,
                        Nome = "Amuleto do V�rtice",
                        Descricao = "Artefato m�gico que reduz o dano m�gico recebido em 15%.",
                        TipoId = "ARTEFATO",
                        IsEquipavel = true,
                        IsConsumivel = false,
                        Dano = 0,
                        Defesa = 10
                    }
            };
            _Inventarios = new List<InventarioRecord>
            {
                new InventarioRecord
                {
                    Id = 1,
                    IdPersonagem = 1, // Kael'thor
                    Descricao = "Invent�rio do Kael'thor",
                    Quantidade = 2,
                    Itens = new List<ItemRecord>
                    {
                        _Itens.First(i => i.Id == 1),
                        _Itens.First(i => i.Id == 4)
                    }
                },
                new InventarioRecord
                {
                    Id = 2,
                    IdPersonagem = 2, // Thorgun
                    Descricao = "Equipamentos principais de Thorgun",
                    Quantidade = 2,
                    Itens = new List<ItemRecord>
                    {
                        _Itens.First(i => i.Id == 2),
                        _Itens.First(i => i.Id == 3)
                    }
                },
                new InventarioRecord
                {
                    Id = 3,
                    IdPersonagem = 5, // Az'Karum
                    Descricao = "Itens m�gicos carregados por Az'Karum",
                    Quantidade = 1,
                    Itens = new List<ItemRecord>
                    {
                        _Itens.First(i => i.Id == 4)
                    }
                }
            };
        }

        #region Private Methods
        ///Aqui se encaixa o m�todo de valida��o dos dados que ser� usados dentro deste mesmo services
        ///
        private static void ValidateParams(InventarioRecord record, bool isUpdate)
        {
            if (record == null) throw new Exception("Dados inv�lidos.");

            if (record.Id <= 0) throw new Exception("O identificador 'Id' n�o pode estar vazio/zerado.");
            if (record.IdPersonagem <= 0) throw new Exception("O identificador 'IdPersonagem' n�o pode estar vazio/zerado.");
            if (record.Descricao == null) throw new Exception("O campo 'Descri��o' � obrigat�rio");
            if (record.Quantidade <= 0) throw new Exception("O campo 'Quantidade' � obrigat�rio e n�o pode ser menor que zero.");

        }
        private static void ValidateItemParams(ItemRecord record, bool isUpdate)
        {
            if (record == null) throw new Exception("Dados inv�lidos.");

            if (record.Id <= 0) throw new Exception("O identificador 'Id' n�o pode estar vazio/zerado.");
            if (record.Nome == null) throw new Exception("O campo Nome' � obrigat�rio");
            if (record.Descricao == null) throw new Exception("O campo 'Descri��o' � obrigat�rio");
            if (record.TipoId == null) throw new Exception("O campo 'TipoId' � obrigat�rio.");


        }

        #endregion Private Methods

        #region Public Methods
        public InventarioRecord GetInventarios(int id)
        {
            try
            {
                var record = _Inventarios.FirstOrDefault(e => e.Id == id);

                if (record == null) throw new Exception("Nenhum registro encontrado com este ID.");
                return record;
            }
            catch
            {
                throw;
            }
        }
        public List<ItemRecord> GetItensList()
        {
            try
            {
                return _Itens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<InventarioRecord> GetFilter(
            int? id,
            int? idPersonagem,
            string? descricao,
            int? quantidade)
        {
            try
            {
                var records = _Inventarios.Where(i => descricao != null ? i.Descricao.Contains(descricao) : true)
                                        .Where(i => quantidade != null ? i.Quantidade == quantidade : true)
                                        .Where(i => id != null ? i.Id == id : true)
                                        .Where(i => idPersonagem != null ? i.IdPersonagem == idPersonagem : true);


                if (records == null || records.Count() == 0) throw new Exception("Nenhum registro encontrado com esta filtragem.");
                return records.ToList();
            }
            catch
            {
                throw;
            }

            #endregion Public Methods
        }

        public void CreateItem(ItemRecord record)
        {
            try
            {
                ValidateItemParams(record, false);

                // Gerar novo ID
                int novoId = _Itens.Max(i => i.Id) + 1;
                record.Id = novoId;

                _Itens.Add(record);
            }
            catch
            {
                throw;
            }
        }
    }
}