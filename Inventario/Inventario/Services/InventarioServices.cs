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
                        Descricao = "Itens usados para causar dano físico ou mágico."
                    },
                    new TipoItemRecord { IdTipoItem = "ARMADURA", Nome = "Armadura", Descricao = "Equipamentos defensivos que aumentam a proteção do personagem." },
                    new TipoItemRecord { IdTipoItem = "POCAO", Nome = "Poção", Descricao = "Itens consumíveis que restauram vida ou fornecem efeitos temporários." },
                    new TipoItemRecord { IdTipoItem = "ARTEFATO", Nome = "Artefato", Descricao = "Itens mágicos raros com propriedades únicas." }
                };
            _Itens = new List<ItemRecord>
            {
                    new ItemRecord
                    {
                        Id = 1,
                        Nome = "Espada Longa de Prata",
                        Descricao = "Uma lâmina reluzente eficaz contra criaturas sombrias.",
                        TipoId = "ARMA",
                        IsEquipavel = true,
                        IsConsumivel = false,
                        Dano = 25,
                        Defesa = 0
                    },
                    new ItemRecord
                    {
                        Id = 2,
                        Nome = "Armadura de Escamas Dracônicas",
                        Descricao = "Feita das escamas de um dragão ancião, oferece excelente proteção.",
                        TipoId = "ARMADURA",
                        IsEquipavel = true,
                        IsConsumivel = false,
                        Dano = 0,
                        Defesa = 35
                    },
                    new ItemRecord
                    {
                        Id = 3,
                        Nome = "Poção de Cura Maior",
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
                        Nome = "Amuleto do Vórtice",
                        Descricao = "Artefato mágico que reduz o dano mágico recebido em 15%.",
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
                    Descricao = "Inventário do Kael'thor",
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
                    Descricao = "Itens mágicos carregados por Az'Karum",
                    Quantidade = 1,
                    Itens = new List<ItemRecord>
                    {
                        _Itens.First(i => i.Id == 4)
                    }
                }
            };
        }

        #region Private Methods
        ///Aqui se encaixa o método de validação dos dados que será usados dentro deste mesmo services
        ///
        private static void ValidateParams(InventarioRecord record, bool isUpdate)
        {
            if (record == null) throw new Exception("Dados inválidos.");

            if (record.Id <= 0) throw new Exception("O identificador 'Id' não pode estar vazio/zerado.");
            if (record.IdPersonagem <= 0) throw new Exception("O identificador 'IdPersonagem' não pode estar vazio/zerado.");
            if (record.Descricao == null) throw new Exception("O campo 'Descrição' é obrigatório");
            if (record.Quantidade <= 0) throw new Exception("O campo 'Quantidade' é obrigatório e não pode ser menor que zero.");

        }
        private static void ValidateItemParams(ItemRecord record, bool isUpdate)
        {
            if (record == null) throw new Exception("Dados inválidos.");

            if (record.Id <= 0) throw new Exception("O identificador 'Id' não pode estar vazio/zerado.");
            if (record.Nome == null) throw new Exception("O campo Nome' é obrigatório");
            if (record.Descricao == null) throw new Exception("O campo 'Descrição' é obrigatório");
            if (record.TipoId == null) throw new Exception("O campo 'TipoId' é obrigatório.");


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