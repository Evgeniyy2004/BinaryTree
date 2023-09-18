using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees;
public class BinaryTree<T> : IEnumerable<T> where T : IComparable
{
    TreeNode<T>? root;
    public void Add(T valuetoadd)
    {
        if (root == null)
        {
            root = new TreeNode<T>() { Value = valuetoadd, Left = null, Right = null, size = 1 };
            return;
        }

        var curr = root;
        while (true)
        {
            curr.size += 1;
            if (curr.Value!.CompareTo(valuetoadd!) <= 0)
            {
                if (curr.Right == null)
                {
                    curr.Right = new TreeNode<T>() { Value = valuetoadd, size = 1 }!;
                    return;
                }

                else curr = curr.Right;
            }

            else
            {
                if (curr.Left == null)
                {
                    curr.Left = new TreeNode<T>() { Value = valuetoadd, size = 1 }!;
                    return;
                }

                else curr = curr.Left!;
            }
        }
    }

    public bool Contains(T element)
    {
        if (this.root == null) return false;
        var curr = root;
        while (true)
        {
            if (curr == null) return false;
            if (element.CompareTo(curr.Value) == 0) return true;
            if (element.CompareTo(curr.Value) < 0) curr = curr.Left;
            else curr = curr.Right;
        }
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        IEnumerable<T> list = TakeItAll(this.root);
        return list!.GetEnumerator();
    }
    public T this[int index]
    {
        get
        {
            int i = 0;
            if (this.root == null) throw new Exception();
            var curr = root;
            return TakeWithIndex(i, curr, index);
        }
    }

    public static T TakeWithIndex(int skipped, TreeNode<T> curr,int index)
    {
        if (curr.Left == null || skipped + curr.Left!.size - 1 < index)
        {
            if (curr.Left == null)
            {
                if (skipped - 1 == index - 1)
                {
                    return curr.Value!;
                }
                skipped += 1;
                if (curr.Right == null) throw new IndexOutOfRangeException();
                curr = curr.Right!;
                return TakeWithIndex(skipped, curr, index);
            }

            if (skipped + curr.Left!.size - 1 == index - 1)
            {
                return curr.Value!;
            }

            else
            {
                if (curr.Right == null) throw new IndexOutOfRangeException();
                skipped += curr.Left!.size + 1;
                curr = curr.Right!;
                return TakeWithIndex(skipped, curr, index); ;
            }
        }

        if (curr.Left == null) throw new IndexOutOfRangeException();
        else curr = curr.Left;
        return TakeWithIndex(skipped, curr, index);
    }
    public static IEnumerable<T> TakeItAll(TreeNode<T> curr)
    {
        if (curr == null) yield break;
        if (curr.Left != null)
        {
            foreach (var t in TakeItAll(curr.Left)!)
            {
                yield return t;
            }
        }

        yield return curr!.Value!;
        if (curr.Right != null)
        {
            foreach (var t in TakeItAll(curr.Right)!)
            {
                yield return t;
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        IEnumerable<T> list = TakeItAll(this.root);
        return list!.GetEnumerator();
    }
}

public class TreeNode<T>
{

    public T? Value;
    public int size = 0;
    public TreeNode<T>? Left;
    public TreeNode<T>? Right;

}