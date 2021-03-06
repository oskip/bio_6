﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio_6
{
     class Program
     {
         static Dictionary<int, List<int>> wyniki =  new Dictionary<int, List<int>>();
        
            
         static String sekwencja = null;

          static void Main(string[] args)
          {
              string wyrazenie = Console.ReadLine();
             
              
              try
              {
                  using (StreamReader sr = new StreamReader("../../sekwencje/in.txt"))
                  {
                      sekwencja = sr.ReadToEnd();
                      
                  }
              }
              catch (Exception e)
              {
                  Console.WriteLine("The file could not be read:");
                  Console.WriteLine(e.Message);
              }

              string[] wyr = wyrazenie.Split('-');

              for (int i = 0; i < wyr.Length; i++ )
              {
                  string w = wyr[i];
                  switch (w[0])
                  {
                      case '[':
                          string a=w.Substring(1,w.Length-2);
                          
                          zNawiasu(a, i);
                          break;
                      case 'V': dowolnaLitera(i);
                          break;
                      case 'x': dowolnyCiag(w, i);
                          break;
                      case '{':
                          string b = w.Substring(1, w.Length - 1); 
                          bezTych(b, i);
                          break;
                      default:
                          if (w.Length == 1)
                          {
                              litera(w, i);
                              break;
                          }
                          else
                          {
                              KRazy(w, i);
                              break;
                          }

                  }
              }


             sprawdzWynik(wyr.Length);

                           


              Console.ReadLine();

          }

         

          private static void sprawdzWynik(int ilosc)
          {
              
              int poczatek;
              int koniec;
              bool w=true;
              bool wypisane = false;
              for (int j = 0; j < wyniki[0].Count; j++)
              {
                  w = true;
                  int pozycja = wyniki[0].ElementAt(j);
                  poczatek = pozycja;
                  int k = 0;
                  if (pozycja == -3)
                  {
                      pozycja = wyniki[0].ElementAt(j + 1) + wyniki[0].ElementAt(j + 2) - 1;
                      j = j + 2;
                      poczatek = wyniki[0].ElementAt(j);
                  }


                  while (k + 1 < ilosc)
                  {

                      {                      
                          pozycja++;
                          if (wyniki[k + 1].Contains(-3) && wyniki[k + 1].Contains(pozycja))
                          {

                              if (wyniki[k + 1].Count - 1 < wyniki[k + 1].IndexOf(pozycja) + 1 || wyniki[k + 1][wyniki[k + 1].IndexOf(pozycja) + 1] == -3)
                              {
                                  pozycja = pozycja + wyniki[k + 1][wyniki[k + 1].IndexOf(pozycja) - 1] - 1;
                              }
                              else
                              {
                                  w = false;
                                  break;
                              }
                          }
                          else
                              if (wyniki[k + 1].Contains(pozycja))
                                  w = true;
                              else
                              {
                                  w = false;
                                  break;
                              }
                      }
                      k++;
                          
                  }
                  koniec = pozycja;
                  if (w == true)
                  {
                      Console.WriteLine(w);
                      Console.WriteLine(poczatek);
                      Console.WriteLine(koniec);
                      wypisane=true;
                  }
                  

                  
              }
                  if (wypisane == false) Console.WriteLine("Nie odnaleziono");

                  
              
          }




          private static void KRazy(string w, int i)
          {
              if(!w.Contains(','))
              {
                  int k;

                  string a = w.Substring(2, w.Length - 3);

                  int.TryParse(a, out k);
                  
                  List<int> pozycja = new List<int>();

                  

                  for (int j = 0; j <= sekwencja.Length - k; j++)
                  {
                      bool rowne = true;
                      for (int c = 0; c < k; c++)
                      {
                          if (sekwencja[j + c] == w[0])
                              rowne = true;
                          else
                          {
                              rowne = false;
                              
                          }
                          if (rowne == false) break;
                      }
                      if (rowne == true)
                      {
                          pozycja.Add(-3);
                          pozycja.Add(k);
                          pozycja.Add(j);
                          
                      }


                   }

                  wyniki.Add(i, pozycja);
                
              }

              else
              {
                  int min, max;

                  string a = w.Substring(2, w.Length - 3);
                  string[] liczby = a.Split(',');

                  int.TryParse(liczby[0], out min);
                  int.TryParse(liczby[1], out max);

                  List<int> pozycja = new List<int>();


                for(int k=min; k<=max; k++)
                  for (int j = 0; j <= sekwencja.Length-k; j++)
                  {
                      bool rowne = true;
                      for (int c = 0; c < k; c++)
                      {
                          if (sekwencja[j + c] == w[0])
                              rowne = true;
                          else
                          {
                              rowne = false;

                          }
                          if (rowne == false) break;
                      }
                      if (rowne == true)
                      {
                          pozycja.Add(-3);
                          pozycja.Add(k);
                          pozycja.Add(j);
                      }


                  }

                  wyniki.Add(i, pozycja);
                
              }
                  
          }

          private static void litera(string w, int i)
          {
              List<int> pozycja = new List<int>();
              
              for(int j=0; j<sekwencja.Length;j++)
              {
                 
                  if(sekwencja[j]==w[0])
                      pozycja.Add(j);
                    
              }

              wyniki.Add(i, pozycja);
          }

          private static void bezTych(string w, int i)
          {
              List<int> pozycja = new List<int>();

              for (int j = 0; j < sekwencja.Length; j++)
              {
                  bool rowne = false;
                  for (int k = 0; k < w.Length; k++)
                      if (sekwencja[j] == w[k])
                          rowne = true;
                  if(rowne==false)
                          pozycja.Add(j);
              }

              wyniki.Add(i, pozycja);
          }

          private static void dowolnyCiag(string w, int i)
          {
              if (w.Contains(','))
              {
                  int min, max;

                  string a = w.Substring(2, w.Length - 3);
                  string[] liczby = a.Split(',');

                  int.TryParse(liczby[0], out min); //to jest i z x(i,j)
                  int.TryParse(liczby[1], out max); //to jest j

                  List<int> pozycja = new List<int>();

                  for (int k = min; k <= max; k++)
                  {
                      for (int j = 0; j <= sekwencja.Length-k;j++ )
                      {
                          pozycja.Add(-3);
                          pozycja.Add(k);
                          pozycja.Add(j);
                      }
                  }

                  wyniki.Add(i, pozycja);
              }

              else if(w.Length>1)
              {
                  int k;

                  string a = w.Substring(2, w.Length - 3);

                  int.TryParse(a, out k);
                  
                  List<int> pozycja = new List<int>();

                  

                  for (int j = 0; j <= sekwencja.Length - k; j++)
                  {
                      
                          pozycja.Add(-3);
                          pozycja.Add(k);
                          pozycja.Add(j);
                          
                      


                   }

                  wyniki.Add(i, pozycja);
              }
              else
              {
                  List<int> pozycja = new List<int>();

                  for (int j = 0; j < sekwencja.Length; j++)
                      pozycja.Add(j);

                  wyniki.Add(i, pozycja);
              }
          }

          private static void dowolnaLitera(int i)
          {
              List<int> pozycja = new List<int>();

              for (int j = 0; j < sekwencja.Length;j++)
                  pozycja.Add(j);

              wyniki.Add(i, pozycja);
          }

          private static void zNawiasu(string w, int i)
          {
              List<int> pozycja = new List<int>();

              for (int j = 0; j < sekwencja.Length; j++)
              {
                  for (int k = 0; k < w.Length; k++)
                      if (sekwencja[j] == w[k])
                          pozycja.Add(j);
              }

              wyniki.Add(i, pozycja);
          }


         
     }
}
