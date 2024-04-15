// João Victor Xavier dos santos
// Jeiel Jedson Leão Alves
// John Lennon

using System;
public class Livro
{
    public int Id;
    public string Titulo;
    public string Autor;
    public int AnoPublicacao;

    public Livro(int id, string titulo, string autor, int anoPublicacao)
    {
        Id = id;
        Titulo = titulo;
        Autor = autor;
        AnoPublicacao = anoPublicacao;
    }
}
public class Node
{
    public Livro Livro;
    public Node Left, Right;
    public int Height;

    public Node(Livro livro)
    {
        Livro = livro;
        Height = 1;
    }
}

public class AVLTree
{
    public Node Root;

    private int Height(Node node)
    {
        if (node == null)
            return 0;
        return node.Height;
    }

    private int Max(int a, int b)
    {
        return (a > b) ? a : b;
    }

    private int GetBalance(Node node)
    {
        if (node == null)
            return 0;
        return Height(node.Left) - Height(node.Right);
    }

    private Node RightRotate(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Max(Height(y.Left), Height(y.Right)) + 1;
        x.Height = Max(Height(x.Left), Height(x.Right)) + 1;

        return x;
    }
    private Node LeftRotate(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Max(Height(x.Left), Height(x.Right)) + 1;
        y.Height = Max(Height(y.Left), Height(y.Right)) + 1;

        return y;
    }
    public Node Insert(Node node, Livro livro)
    {
        if (node == null)
            return new Node(livro);

        if (livro.Id < node.Livro.Id)
            node.Left = Insert(node.Left, livro);
        else if (livro.Id > node.Livro.Id)
            node.Right = Insert(node.Right, livro);
        else 
            return node;

        node.Height = 1 + Max(Height(node.Left), Height(node.Right));

        int balance = GetBalance(node);

        // Left Left Case
        if (balance > 1 && livro.Id < node.Left.Livro.Id)
            return RightRotate(node);

        // Right Right Case
        if (balance < -1 && livro.Id > node.Right.Livro.Id)
            return LeftRotate(node);

        // Left Right Case
        if (balance > 1 && livro.Id > node.Left.Livro.Id)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        // Right Left Case
        if (balance < -1 && livro.Id < node.Right.Livro.Id)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    public void InOrder(Node root)
    {
        if (root != null)
        {
            InOrder(root.Left);
            Console.WriteLine($"ID: {root.Livro.Id}, Título: {root.Livro.Titulo}, Autor: {root.Livro.Autor}, Ano de Publicação: {root.Livro.AnoPublicacao}");
            InOrder(root.Right);
        }
    }
    public Node Search(Node root, int id)
    {
        if (root == null || root.Livro.Id == id)
            return root;

        if (root.Livro.Id < id)
            return Search(root.Right, id);

        return Search(root.Left, id);
    }

    public Node DeleteNode(Node root, int id)
    {
        if (root == null)
            return root;

        if (id < root.Livro.Id)
            root.Left = DeleteNode(root.Left, id);

        else if (id > root.Livro.Id)
            root.Right = DeleteNode(root.Right, id);

        else
        {
            if (root.Left == null || root.Right == null)
            {
                Node temp = null;
                if (temp == root.Left)
                    temp = root.Right;
                else
                    temp = root.Left;

                if (temp == null)
                {
                    temp = root;
                    root = null;
                }
                else 
                    root = temp;
            }
            else
            {
                Node temp = MinValueNode(root.Right);

                root.Livro = temp.Livro;

                root.Right = DeleteNode(root.Right, temp.Livro.Id);
            }
        }

        if (root == null)
            return root;

        root.Height = 1 + Max(Height(root.Left), Height(root.Right));

        int balance = GetBalance(root);

        // Left Left Case
        if (balance > 1 && GetBalance(root.Left) >= 0)
            return RightRotate(root);

        // Left Right Case
        if (balance > 1 && GetBalance(root.Left) < 0)
        {
            root.Left = LeftRotate(root.Left);
            return RightRotate(root);
        }

        // Right Right Case
        if (balance < -1 && GetBalance(root.Right) <= 0)
            return LeftRotate(root);

        // Right Left Case
        if (balance < -1 && GetBalance(root.Right) > 0)
        {
            root.Right = RightRotate(root.Right);
            return LeftRotate(root);
        }

        return root;
    }
    private Node MinValueNode(Node node)
    {
        Node current = node;
        while (current.Left != null)
            current = current.Left;
        return current;
    }

    public void PrintTree(Node root, string indent, bool last)
    {
        if (root != null)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("└─");
                indent += "  ";
            }
            else
            {
                Console.Write("├─");
                indent += "| ";
            }
            Console.WriteLine($"ID: {root.Livro.Id}, Título: {root.Livro.Titulo}, Autor: {root.Livro.Autor}, Ano de Publicação: {root.Livro.AnoPublicacao}");

            PrintTree(root.Left, indent, false);
            PrintTree(root.Right, indent, true);
        }
    }
    public int GetRootBalanceFactor()
    {
        if (Root == null)
            return 0;
        else
            return GetBalance(Root);
    }
}
public class BinarySearchTree
{
    private Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    public void Insert(Livro livro)
    {
        root = InsertRec(root, livro);
    }
    private Node InsertRec(Node root, Livro livro)
    {
        if (root == null)
            return new Node(livro);

        if (livro.Id < root.Livro.Id)
            root.Left = InsertRec(root.Left, livro);
        else if (livro.Id > root.Livro.Id)
            root.Right = InsertRec(root.Right, livro);

        return root;
    }
    public void InOrderTraversal()
    {
        InOrderRec(root);
        Console.WriteLine();
    }
    private void InOrderRec(Node root)
    {
        if (root != null)
        {
            InOrderRec(root.Left);
            Console.WriteLine($"ID: {root.Livro.Id}, Título: {root.Livro.Titulo}, Autor: {root.Livro.Autor}, Ano de Publicação: {root.Livro.AnoPublicacao}");
            InOrderRec(root.Right);
        }
    }
    public void PrintTree()
    {
        if (root == null)
        {
            Console.WriteLine("A árvore está vazia");
            return;
        }

        PrintTreeRec(root, 0);
    }
    private void PrintTreeRec(Node root, int space)
    {
        int COUNT = 10; 
        if (root == null)
            return;

        space += COUNT;

        PrintTreeRec(root.Right, space);

        Console.WriteLine();
        for (int i = COUNT; i < space; i++)
            Console.Write(" ");
        Console.WriteLine($"ID: {root.Livro.Id}, Título: {root.Livro.Titulo}, Autor: {root.Livro.Autor}, Ano de Publicação: {root.Livro.AnoPublicacao}");

        PrintTreeRec(root.Left, space);
    }
    public bool Find(int id)
    {
        Node current = root;
        while (current != null)
        {
            if (id < current.Livro.Id)
            {
                current = current.Left;
            }
            else if (id > current.Livro.Id)
            {
                current = current.Right;
            }
            else
            {
                return true;
            }
        }
        return false; 
    }
    public void Delete(int id)
    {
        root = DeleteRec(root, id);
    }

    private Node DeleteRec(Node root, int id)
    {
        if (root == null) return root;

        if (id < root.Livro.Id)
            root.Left = DeleteRec(root.Left, id);
        else if (id > root.Livro.Id)
            root.Right = DeleteRec(root.Right, id);
        else
        {
            if (root.Left == null)
                return root.Right;
            else if (root.Right == null)
                return root.Left;

            root.Livro = MinValue(root.Right);

            root.Right = DeleteRec(root.Right, root.Livro.Id);
        }

        return root;
    }

    private Livro MinValue(Node root)
    {
        Livro minValue = root.Livro;
        while (root.Left != null)
        {
            minValue = root.Left.Livro;
            root = root.Left;
        }
        return minValue;
    }
    private int Height(Node root)
    {
        if (root == null) return -1;
        return 1 + Math.Max(Height(root.Left), Height(root.Right));
    }
    public int BalanceFactor()
    {
        return BalanceFactor(root);
    }

    private int BalanceFactor(Node root)
    {
        if (root == null) return 0;
        return Height(root.Left) - Height(root.Right);
    }
}
public class BibliotecaManager
{
    private AVLTree avlTree;
    private BinarySearchTree abbTree;
    private int proximoId;

    public BibliotecaManager()
    {
        avlTree = new AVLTree();
        abbTree = new BinarySearchTree();
        proximoId = 1;
    }
    public void AdicionarLivro(string titulo, string autor, int anoPublicacao)
    {
        Livro livro = new Livro(proximoId++, titulo, autor, anoPublicacao);
        avlTree.Root = avlTree.Insert(avlTree.Root, livro);
        abbTree.Insert(livro);
        Console.WriteLine("Livro adicionado com sucesso!");
    }
    public void RemoverLivro(int id)
    {
        Node foundNode = avlTree.Search(avlTree.Root, id);
        if (foundNode != null)
        {
            avlTree.Root = avlTree.DeleteNode(avlTree.Root, id);
            abbTree.Delete(id);
            Console.WriteLine("Livro removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }
    public void BuscarLivroPorId(int id)
    {
        Node foundNode = avlTree.Search(avlTree.Root, id);
        if (foundNode != null)
        {
            Console.WriteLine($"Livro encontrado: ID: {foundNode.Livro.Id}, Título: {foundNode.Livro.Titulo}, Autor: {foundNode.Livro.Autor}, Ano de Publicação: {foundNode.Livro.AnoPublicacao}");
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }
    public void BuscarLivroPorTituloPublico(string titulo)
    {
        Console.WriteLine($"Livros encontrados para o título \"{titulo}\":");
        BuscarLivroPorTituloPrivado(avlTree.Root, titulo);
    }

    private void BuscarLivroPorTituloPrivado(Node node, string titulo)
    {
        if (node != null)
        {
            BuscarLivroPorTituloPrivado(node.Left, titulo);
            if (node.Livro.Titulo.ToLower().Contains(titulo.ToLower()))
            {
                Console.WriteLine($"ID: {node.Livro.Id}, Título: {node.Livro.Titulo}, Autor: {node.Livro.Autor}, Ano de Publicação: {node.Livro.AnoPublicacao}");
            }
            BuscarLivroPorTituloPrivado(node.Right, titulo);
        }
    }
    public void BuscarLivroPorAutorPublico(string autor)
    {
        Console.WriteLine($"\nLivros encontrados para o autor \"{autor}\":");
        BuscarLivroPorAutorPrivado(avlTree.Root, autor);
    }
    private void BuscarLivroPorAutorPrivado(Node node, string autor)
    {
        if (node != null)
        {
            BuscarLivroPorAutorPrivado(node.Left, autor);
            if (node.Livro.Autor == autor)
            {
                Console.WriteLine($"ID: {node.Livro.Id}, Título: {node.Livro.Titulo}, Autor: {node.Livro.Autor}, Ano de Publicação: {node.Livro.AnoPublicacao}");
            }
            BuscarLivroPorAutorPrivado(node.Right, autor);
        }
    }
    public void ListarLivrosPorId()
    {
        Console.WriteLine("Lista de livros em ordem de ID:");
        avlTree.InOrder(avlTree.Root);
    }
    public void MostrarFatorBalanceamento()
    {
        Console.WriteLine($"Fator de balanceamento da árvore AVL: {avlTree.GetRootBalanceFactor()}");
        Console.WriteLine($"Fator de balanceamento da árvore ABB: {abbTree.BalanceFactor()}");
    }
    public void MostrarArvores()
    {
        BibliotecaManager biblioteca = new BibliotecaManager();

        Console.WriteLine("Árvore AVL:");
        biblioteca.avlTree.PrintTree(biblioteca.avlTree.Root, "", true);

        Console.WriteLine("\nÁrvore ABB:");
        biblioteca.abbTree.PrintTree();
    }
    public void MostrarMenu()
    {
        Console.WriteLine("======= MENU =======");
        Console.WriteLine("1. Adicionar Livro");
        Console.WriteLine("2. Remover Livro");
        Console.WriteLine("3. Buscar Livro por ID");
        Console.WriteLine("4. Buscar Livro por Título");
        Console.WriteLine("5. Buscar Livro por Autor");
        Console.WriteLine("6. Listar Livros por ID");
        Console.WriteLine("7. Mostrar Fator de Balanceamento");
        Console.WriteLine("8. Sair");
        Console.WriteLine("====================");
    }
    public void Executar()
    {
        int escolha;
        do
        {
            MostrarMenu();
            Console.Write("Escolha uma opção: ");
            escolha = int.Parse(Console.ReadLine());
            switch (escolha)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("ADIVIONAR LIVRO!");
                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();
                    Console.Write("Ano de Publicação: ");
                    int anoPublicacao = int.Parse(Console.ReadLine());
                    AdicionarLivro(titulo, autor, anoPublicacao);
                    Console.WriteLine("Pressione Qualquer tecla para voltar");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    Console.Write("ID do Livro a Remover: ");
                    int id = int.Parse(Console.ReadLine());
                    RemoverLivro(id);
                    Console.WriteLine("Pressione Qualquer tecla para voltar");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Console.Write("ID do Livro a Buscar: ");
                    int idBuscar = int.Parse(Console.ReadLine());
                    BuscarLivroPorId(idBuscar);
                    Console.WriteLine("Pressione Qualquer tecla para voltar");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    Console.Write("Título do Livro a Buscar: ");
                    string tituloBuscar = Console.ReadLine();
                    BuscarLivroPorTituloPublico(tituloBuscar);
                    Console.WriteLine("Pressione Qualquer tecla para voltar");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 5:
                    Console.Clear();
                    Console.Write("Autor do Livro a Buscar: ");
                    string autorBuscar = Console.ReadLine();
                    BuscarLivroPorAutorPublico(autorBuscar);
                    Console.WriteLine("Pressione Qualquer tecla para voltar");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 6:
                    Console.Clear();
                    ListarLivrosPorId();
                    Console.WriteLine("Pressione Qualquer tecla para voltar");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 7:
                    Console.Clear();
                    MostrarFatorBalanceamento();
                    Console.WriteLine("Pressione Qualquer tecla para voltar");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 8:
                    Console.Clear();
                    Console.WriteLine("Encerrando o programa...");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opção inválida.");
                    Console.WriteLine("Pressione Qualquer tecla para voltar");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        } while (escolha != 8);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BibliotecaManager biblioteca = new BibliotecaManager();
        biblioteca.Executar();
    }
}
