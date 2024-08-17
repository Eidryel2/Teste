using System;
using System.Collections.Generic;


public class Item
{
    public string Nome { get; set; }

    public Item(string nome)
    {
        Nome = nome;
    }

    public override string ToString()
    {
        return Nome;
    }
}


public class Posicao
{
    public Item Item { get; set; }

    public Posicao()
    {
        Item = null;
    }

    public bool EstaVazia()
    {
        return Item == null;
    }

    public void AdicionarItem(Item item)
    {
        if (EstaVazia())
        {
            Item = item;
        }
        else
        {
            throw new InvalidOperationException("A posição já está ocupada.");
        }
    }

    public void RemoverItem()
    {
        if (!EstaVazia())
        {
            Item = null;
        }
        else
        {
            throw new InvalidOperationException("A posição já está vazia.");
        }
    }

    public override string ToString()
    {
        return Item != null ? Item.ToString() : "Vazio";
    }
}


public class Container
{
    public List<Posicao> Posicoes { get; private set; }

    public Container()
    {
        Posicoes = new List<Posicao>();
        for (int i = 0; i < 4; i++)
        {
            Posicoes.Add(new Posicao());
        }
    }

    public bool EstaCheio()
    {
        return Posicoes.TrueForAll(p => !p.EstaVazia());
    }

    public bool EstaVazio()
    {
        return Posicoes.TrueForAll(p => p.EstaVazia());
    }

    public void AdicionarItem(int posicao, Item item)
    {
        if (EstaCheio())
        {
            throw new InvalidOperationException("O container está cheio.");
        }
        Posicoes[posicao].AdicionarItem(item);
    }

    public void RemoverItem(int posicao)
    {
        Posicoes[posicao].RemoverItem();
    }

    public void AdicionarItens(Item item)
    {
        foreach (var pos in Posicoes)
        {
            if (pos.EstaVazia())
            {
                pos.AdicionarItem(item);
                return;
            }
        }
    }

    public void RemoverItens()
    {
        if (EstaVazio())
        {
            throw new InvalidOperationException("O container já está vazio.");
        }

        foreach (var pos in Posicoes)
        {
            pos.RemoverItem();
        }
    }
}


public class Andar
{
    public string Tipo { get; private set; }
    public List<Container> Containers { get; private set; }

    public Andar(string tipo)
    {
        Tipo = tipo;
        Containers = new List<Container>
        {
            new Container(),
            new Container()
        };
    }
}


public class Geladeira
{
    public List<Andar> Andares { get; private set; }

    public Geladeira()
    {
        Andares = new List<Andar>
        {
            new Andar("Hortifrutis"),
            new Andar("Laticínios e Enlatados"),
            new Andar("Charcutaria, Carnes e Ovos")
        };
    }


    public void AdicionarItem(int andar, int container, int posicao, Item item)
    {
        try
        {
            Andares[andar].Containers[container].AdicionarItem(posicao, item);
            Console.WriteLine($"Item '{item.Nome}' adicionado ao andar {andar}, container {container}, posição {posicao}.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


    public void RemoverItem(int andar, int container, int posicao)
    {
        try
        {
            Andares[andar].Containers[container].RemoverItem(posicao);
            Console.WriteLine($"Item removido do andar {andar}, container {container}, posição {posicao}.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void AdicionarItensAoContainer(int andar, int container, Item item)
    {
        try
        {
            Andares[andar].Containers[container].AdicionarItens(item);
            Console.WriteLine($"Item '{item.Nome}' adicionado ao container {container} no andar {andar}.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


    public void RemoverItensDoContainer(int andar, int container)
    {
        try
        {
            Andares[andar].Containers[container].RemoverItens();
            Console.WriteLine($"Todos os itens removidos do container {container} no andar {andar}.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


    public void ExibirItens()
    {
        for (int i = 0; i < Andares.Count; i++)
        {
            Console.WriteLine($"Andar {i} ({Andares[i].Tipo}):");
            for (int j = 0; j < Andares[i].Containers.Count; j++)
            {
                Console.WriteLine($"  Container {j}:");
                for (int k = 0; k < Andares[i].Containers[j].Posicoes.Count; k++)
                {
                    Console.WriteLine($"    Posição {k}: {Andares[i].Containers[j].Posicoes[k]}");
                }
            }
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        Geladeira minhaGeladeira = new Geladeira();
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("\nMenu de Operações da Geladeira:");
            Console.WriteLine("1. Adicionar item em uma posição específica");
            Console.WriteLine("2. Remover item de uma posição específica");
            Console.WriteLine("3. Adicionar item a um container");
            Console.WriteLine("4. Remover todos os itens de um container");
            Console.WriteLine("5. Exibir todos os itens da geladeira");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Digite o andar (0 a 2): ");
                    int andarAdd = int.Parse(Console.ReadLine());
                    Console.Write("Digite o container (0 a 1): ");
                    int containerAdd = int.Parse(Console.ReadLine());
                    Console.Write("Digite a posição (0 a 3): ");
                    int posicaoAdd = int.Parse(Console.ReadLine());
                    Console.Write("Digite o nome do item: ");
                    string nomeItemAdd = Console.ReadLine();
                    minhaGeladeira.AdicionarItem(andarAdd, containerAdd, posicaoAdd, new Item(nomeItemAdd));
                    break;

                case "2":
                    Console.Write("Digite o andar (0 a 2): ");
                    int andarRem = int.Parse(Console.ReadLine());
                    Console.Write("Digite o container (0 a 1): ");
                    int containerRem = int.Parse(Console.ReadLine());
                    Console.Write("Digite a posição (0 a 3): ");
                    int posicaoRem = int.Parse(Console.ReadLine());
                    minhaGeladeira.RemoverItem(andarRem, containerRem, posicaoRem);
                    break;

                case "3":
                    Console.Write("Digite o andar (0 a 2): ");
                    int andarAddCont = int.Parse(Console.ReadLine());
                    Console.Write("Digite o container (0 a 1): ");
                    int containerAddCont = int.Parse(Console.ReadLine());
                    Console.Write("Digite o nome do item: ");
                    string nomeItemAddCont = Console.ReadLine();
                    minhaGeladeira.AdicionarItensAoContainer(andarAddCont, containerAddCont, new Item(nomeItemAddCont));
                    break;

                case "4":
                    Console.Write("Digite o andar (0 a 2): ");
                    int andarRemCont = int.Parse(Console.ReadLine());
                    Console.Write("Digite o container (0 a 1): ");
                    int containerRemCont = int.Parse(Console.ReadLine());
                    minhaGeladeira.RemoverItensDoContainer(andarRemCont, containerRemCont);
                    break;

                case "5":
                    minhaGeladeira.ExibirItens();
                    break;

                case "6":
                    sair = true;
                    break;

                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }
        }
    }
}
