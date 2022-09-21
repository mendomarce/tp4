using System;
using System.Collections.Generic; // para utilziar lista
namespace tp4 {
  public interface Comparable // hacer la interface publica permite usarla en cualquier namespace.
  {
    bool sosIgual(Comparable c); //toda interface se asume public. Esta bien sin especificar
    bool sosMenor(Comparable c);
    bool sosMayor(Comparable c);
  }
  // actividad 1 tp2
  ////////////// Patron de diseño Strategy para Alumnos
  public interface StrategyAlumnos {
    bool sosIgual(Comparable c, Comparable thisAlumno);
    bool sosMayor(Comparable c, Comparable thisAlumno);
    bool sosMenor(Comparable c, Comparable thisAlumno);
  }
  public class StrategyAlumnosPorDNI: StrategyAlumnos {
    public bool sosIgual(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getDNI() == ((Alumno) c).getDNI();
    }
    public bool sosMayor(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getDNI() > ((Alumno) c).getDNI();
    }
    public bool sosMenor(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getDNI() < ((Alumno) c).getDNI();
    }
  }
  public class StrategyAlumnosPorLegajo: StrategyAlumnos {
    public bool sosIgual(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getLegajo() == ((Alumno) c).getLegajo();
    }
    public bool sosMayor(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getLegajo() > ((Alumno) c).getLegajo();
    }
    public bool sosMenor(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getLegajo() < ((Alumno) c).getLegajo();
    }
  }
  public class StrategyAlumnosPorPromedio: StrategyAlumnos {
    public bool sosIgual(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getPromedio() == ((Alumno) c).getPromedio();
    }
    public bool sosMayor(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getPromedio() > ((Alumno) c).getPromedio();
    }
    public bool sosMenor(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getPromedio() < ((Alumno) c).getPromedio();
    }

  }
  public class StrategyAlumnosPorNombre: StrategyAlumnos {
    public bool sosIgual(Comparable c, Comparable thisAlumno) {
      return ((Alumno) thisAlumno).getNombre().ToLower() == ((Alumno) c).getNombre().ToLower();
    }
    public bool sosMayor(Comparable c, Comparable thisAlumno) {
      string thisAlumnoNombre = ((Alumno) thisAlumno).getNombre().ToLower();
      string cNombre = ((Alumno) c).getNombre().ToLower();
      string menor = Program.ordenAlfa(thisAlumnoNombre, cNombre);
      if (menor == cNombre && thisAlumnoNombre != cNombre) {
        return true;
      }
      return false;
    }
    public bool sosMenor(Comparable c, Comparable thisAlumno) {
      string thisAlumnoNombre = ((Alumno) thisAlumno).getNombre().ToLower();
      string cNombre = ((Alumno) c).getNombre().ToLower();
      string menor = Program.ordenAlfa(thisAlumnoNombre, cNombre);
      if (menor == thisAlumnoNombre && thisAlumnoNombre != cNombre) {
        return true;
      }
      return false;
    }
  }
  /////////////////////////////////////////////
  public class Numero: Comparable {
    private int valor;
    public Numero(int v) {
      valor = v;
    }
    public int getValor() {
      return valor;
    }


    public bool sosIgual(Comparable c) // no hace falta usar override
    {
      return this.valor == ((Numero) c).getValor(); // Es necesario castear el comparable a numero ya que comparable no tiene un metodo getValor
    }
    public bool sosMenor(Comparable c) {
      return this.valor < ((Numero) c).getValor(); // Es necesario castear el comparable a numero ya que comparable no tiene un metodo getValor
    }
    public bool sosMayor(Comparable c) {
      return this.valor > ((Numero) c).getValor();
    }
    public override string ToString() // el metodo ToString existe en un metodo de object que aplica a todos los objetos y por eso esnecesario sobreescribirlo
    {
      return valor.ToString();
    }
  }
  public interface Coleccionable {
    int cuantos();
    Comparable minimo();
    Comparable maximo();
    void agregar(Comparable c);
    bool contiene(Comparable c);
  }
  public class Pila: Coleccionable, Iterable {
    private List < Comparable > elementos;

    public Pila() {
      elementos = new List < Comparable > ();
    }
    public void push(Comparable c) {
      elementos.Add(c);
    }
    public Comparable pop() {
      Comparable retorno = elementos[elementos.Count - 1];
      elementos.RemoveAt(elementos.Count - 1);
      return retorno;
    }
    public int cuantos() {
      return elementos.Count;
    }
    public Comparable minimo() {
      Comparable menor = elementos[0];
      for (int i = 1; i < elementos.Count; i++) {
        if (elementos[i].sosMenor(menor)) {
          menor = elementos[i];
        }
      }
      return menor;
    }
    public Comparable maximo() {
      Comparable mayor = elementos[0];
      for (int i = 1; i < elementos.Count; i++) {
        if (elementos[i].sosMayor(mayor)) {
          mayor = elementos[i];
        }
      }
      return mayor;
    }
    public void agregar(Comparable c) {
      elementos.Add(c);
    }
    public bool contiene(Comparable c) {
      for (int i = 0; i < elementos.Count; i++) {
        if (c.sosIgual(elementos[i])) // si aca usara c=elementos[i] me daria falso aunque los valores que quiero comparar son iguales, esto es pq == comapara posiciones de mempria, si las pos de memoria son distintas entonces da false
        {
          return true;
        }
      }
      return false;
    }
    public Iterador getIterador() {
      return new IteradorDePila(elementos);
    }
  }
  public class Cola: Coleccionable, Iterable {
    private List < Comparable > elementos;
    public Cola() {
      elementos = new List < Comparable > ();
    }
    public void push(Comparable c) {
      elementos.Add(c);
    }
    public Comparable pop() {
      Comparable retorno = elementos[0];
      elementos.RemoveAt(0);
      return retorno;
    }
    public int cuantos() {
      return elementos.Count;
    }
    public Comparable minimo() {
      Comparable menor = elementos[0];
      for (int i = 1; i < elementos.Count; i++) {
        if (elementos[i].sosMenor(menor)) {
          menor = elementos[i];
        }
      }
      return menor;
    }
    public Comparable maximo() {
      Comparable mayor = elementos[0];
      for (int i = 1; i < elementos.Count; i++) {
        if (elementos[i].sosMayor(mayor)) {
          mayor = elementos[i];
        }
      }
      return mayor;
    }
    public void agregar(Comparable c) {
      elementos.Add(c);
    }
    public bool contiene(Comparable c) {
      for (int i = 0; i < elementos.Count; i++) {
        if (c.sosIgual(elementos[i])) // si aca usara c=elementos[i] me daria falso aunque los valores que quiero comparar son iguales, esto es pq == comapara posiciones de mempria, si las pos de memoria son distintas entonces da false
        {
          return true;
        }
      }
      return false;
    }
    public Iterador getIterador() {
      return new IteradorDeCola(elementos);
    }
  }
  public class ColeccionMultiple: Coleccionable {
    private Pila pila; //va privado???
    private Cola cola; //va privado???
    public ColeccionMultiple(Pila p, Cola c) {
      pila = p;
      cola = c;
    }
    public int cuantos() {
      return pila.cuantos() + cola.cuantos();
    }
    public Comparable minimo() {
      Comparable p = pila.minimo();
      Comparable c = cola.minimo();
      if (p.sosMenor(c)) {
        return p;
      }
      return c;
    }
    public Comparable maximo() {
      Comparable p = pila.maximo();
      Comparable c = cola.maximo();
      if (p.sosMayor(c)) {
        return p;
      }
      return c;
    }
    public void agregar(Comparable d) {}
    public bool contiene(Comparable d) {
      return pila.contiene(d) || cola.contiene(d);
    }
  }
  public class Persona: Comparable {
    protected string nombre;
    protected int dni;
    public Persona(string n, int d) {
      nombre = n;
      dni = d;
    }
    public string getNombre() {
      return nombre;
    }
    public int getDNI() {
      return dni;
    }
    public virtual bool sosIgual(Comparable c) // no hace falta usar override
    {
      return this.dni == ((Persona) c).getDNI(); // Es necesario castear el comparable a numero ya que comparable no tiene un metodo getValor
    }
    public virtual bool sosMenor(Comparable c) {
      return this.dni < ((Persona) c).getDNI(); // Es necesario castear el comparable a numero ya que comparable no tiene un metodo getValor
    }
    public virtual bool sosMayor(Comparable c) {
      return this.dni > ((Persona) c).getDNI();
    }
    public override string ToString() // el metodo ToString existe en un metodo de object que aplica a todos los objetos y por eso esnecesario sobreescribirlo
    {
      return "DNI: " + dni.ToString() + ", Nombre: " + nombre.ToString();
    }
  }
  public class Alumno: Persona {
    private int legajo;
    private float promedio;
    private StrategyAlumnos estrategia;
    public Alumno(string n, int d, int l, float p): base(n, d) {
      legajo = l;
      promedio = p;
      estrategia = new StrategyAlumnosPorPromedio(); // estrategia default
    }
    public int getLegajo() {
      return legajo;
    }
    public float getPromedio() {
      return promedio;
    }
    public void setEstrategia(StrategyAlumnos a) {
      this.estrategia = a;
    }
    public StrategyAlumnos getEstrategia() {
      return this.estrategia;
    }
    public override bool sosIgual(Comparable c) // no hace falta usar override
    {
      return estrategia.sosIgual(c, this);
    }
    public override bool sosMenor(Comparable c) {
      return estrategia.sosMenor(c, this);
    }
    public override bool sosMayor(Comparable c) {
      return estrategia.sosMayor(c, this);
    }
    public override string ToString() // el metodo ToString existe en un metodo de object que aplica a todos los objetos y por eso esnecesario sobreescribirlo
    {
      return "DNI: " + dni.ToString() + ", Nombre: " + nombre.ToString() + ", Legajo: " + legajo.ToString() + ", Promedio: " + promedio.ToString();
    }
  }
  //actividad 3 y 5 del tp2
  public class Conjunto: Coleccionable, Iterable {
    private List < Comparable > conjunto;

    public Conjunto() {
      conjunto = new List < Comparable > ();
    }
    public Iterador getIterador() {
      return new IteradorDeCola(conjunto);
    }
    public bool pertenece(Comparable elemento) {
      return conjunto.Contains(elemento);
    }
    public int cuantos() {
      return conjunto.Count;
    }
    public Comparable minimo() {
      Comparable menor = conjunto[0];

      for (int i = 1; i < conjunto.Count; i++) {
        if (conjunto[i].sosMenor(menor)) {
          menor = conjunto[i];
        }
      }
      return menor;
    }
    public Comparable maximo() {
      Comparable mayor = conjunto[0];

      for (int i = 1; i < conjunto.Count; i++) {
        if (conjunto[i].sosMayor(mayor)) {
          mayor = conjunto[i];
        }
      }
      return mayor;
    }
    public void agregar(Comparable c) {
      if (!(this.contiene(c))) {
        conjunto.Add(c);
      }
    }
    public bool contiene(Comparable c) {
      for (int i = 0; i < conjunto.Count; i++) {
        if (c.sosIgual(conjunto[i])) // si aca usara c=elementos[i] me daria falso aunque los valores que quiero comparar son iguales, esto es pq == comapara posiciones de mempria, si las pos de memoria son distintas entonces da false
        {
          return true;
        }
      }
      return false;
    }
  }
  // actividad 4 tp2 (usar conjunto para agregar pares, no entiendo como)
  public class Diccionario: Iterable, Coleccionable {
    List < ClaveValor > diccionario;
    public Diccionario() {
      diccionario = new List < ClaveValor > ();
    }
    public void agregar(Comparable clave, Comparable valor) {
      for (int i = 0; i < diccionario.Count; i++) {
        if (diccionario[i].Clave.sosIgual(clave)) {
          diccionario[i].Valor=valor;
          return;
        }
      }
      diccionario.Add(new ClaveValor(clave,valor));
    }
    public Comparable valorDe(Comparable clave) {
      foreach(ClaveValor cv in diccionario) {
        if (cv.Clave.sosIgual(clave)) {
          return cv.Valor;
        }
      }
      return null;
    }
    public Iterador getIterador() {
      return new IteradorDeDiccionario(diccionario);
    }
    public int cuantos() {
      return diccionario.Count;
    }
    public Comparable minimo() {
      Comparable menor = diccionario[0].Valor;
      for (int i = 1; i < diccionario.Count; i++) {
        if (diccionario[i].Valor.sosMenor(menor)) {
          menor = diccionario[i].Valor;
        }
      }
      return menor;
    }
    public Comparable maximo() {
      Comparable mayor = diccionario[0].Valor;
      for (int i = 1; i < diccionario.Count; i++) {
        if (diccionario[i].Valor.sosMayor(mayor)) {
          mayor = diccionario[i].Valor;
        }
      }
      return mayor;
    }
    public void agregar(Comparable c) {
      Numero claveUnica = new Numero(Program.rnd.Next(0, 100000000));
      for (int i = 0; i < diccionario.Count; i++) {
        if (claveUnica.sosIgual(diccionario[i].Clave)) {
          claveUnica = new Numero(Program.rndRandom(0, 100000000));
          i = 0;
        }
      }
      agregar(claveUnica, c);
    }
    public bool contiene(Comparable c) {
      for (int i = 0; i < diccionario.Count; i++) {
        if (c.sosIgual(diccionario[i].Valor)) // si aca usara c=elementos[i] me daria falso aunque los valores que quiero comparar son iguales, esto es pq == comapara posiciones de mempria, si las pos de memoria son distintas entonces da false
        {
          return true;
        }
      }
      return false;
    }
  }
  public class ClaveValor: Comparable {
    Comparable valor;
    Comparable clave;
    public ClaveValor(Comparable clave, Comparable valor) {
      this.clave = clave;
      this.valor = valor;
    }
    public Comparable Valor {
      set {
        valor = value;
      }
      get {
        return valor;
      }
    }
    public Comparable Clave {
      set {
        clave = value;
      }
      get {
        return clave;
      }
    }
    public bool sosIgual(Comparable c) {
      return this.Valor.sosIgual(((ClaveValor) c).Valor);
    }

    public bool sosMayor(Comparable c) {
      return this.Valor.sosMayor(((ClaveValor) c).Valor);
    }
    public bool sosMenor(Comparable c) {
      return this.Valor.sosMenor(((ClaveValor) c).Valor);
    }
    public override string ToString() {
      return "Clave " + clave.ToString() + " Valor (" + valor.ToString() + ")";
    }
  }
  //actividad 6 del tp2
  public interface Iterable {
    Iterador getIterador();
  }
  public interface Iterador {
    void primero();
    void siguiente();
    bool fin();
    Comparable actual();
  }
  public class IteradorDePila: Iterador {
    List < Comparable > lista;
    int indice = 0;
    public IteradorDePila(List < Comparable > lista) {
      this.lista = lista;
    }
    public void primero() {
      indice = lista.Count - 1;
    }
    public void siguiente() {
      indice--;
    }
    public bool fin() {
      return indice < 0;
    }
    public Comparable actual() {
      return lista[indice];
    }
  }
  public class IteradorDeCola: Iterador {
    List < Comparable > lista;
    int indice = 0;
    public IteradorDeCola(List < Comparable > lista) {
      this.lista = lista;
    }
    public void primero() {
      indice = 0;
    }
    public void siguiente() {
      indice++;
    }
    public bool fin() {
      return indice >= lista.Count;
    }
    public Comparable actual() {
      return lista[indice];
    }
  }
  public class IteradorDeDiccionario: Iterador {
    List < ClaveValor > lista;
    int indice = 0;
    public IteradorDeDiccionario(List < ClaveValor > lista) {
      this.lista = lista;
    }
    public void primero() {
      indice = 0;
    }
    public void siguiente() {
      indice++;
    }
    public bool fin() {
      return indice >= lista.Count;
    }
    public Comparable actual() {
      return (ClaveValor) lista[indice];
    }
  }
  // actividad 2 tp3
  public class GeneradorDeDatosAleatorios
  {
    public static int numerosAleatorios (int max)
    {
      return Program.rnd.Next(0,max);
    }
    public static string stringAleatorio (int cant)
    {
      string retorno= "";
      for (int i = 0; cant > i; i++) {
        retorno+= (char) Program.rnd.Next(97, 123);
      }
      return retorno;
    }
  }
  // actividad 3 tp3
  public class LectordeDatos
  {
    public static float numeroPorTeclado()
    {
      return float.Parse(Console.ReadLine());
    }
    public static string stringPorTeclado()
    {
      return Console.ReadLine();
    }
  }
  // actividad 4 tp3
  public abstract class FabricaDeComparables{
    public const int vendedor=0;
    public const int alumno=1;
    public const int numero=2;

    public static int fabricaPorDefecto=numero;
    

  public abstract Comparable crearAleatorio();
  public static Comparable crearAleatorio(int opcion)
  {
    FabricaDeComparables fabrica;
    switch (opcion){
      case numero:{
        fabrica=new FabricaDeNumeros();
        break;
      }
      case alumno:{
        fabrica= new FabricaDeAlumnos();
        break;
      }
      case vendedor:{
        fabrica= new FabricaDeVendedores();
        break;
      }
      default:{

       fabrica= new FabricaDeNumeros();break;
      }
    }
    return fabrica.crearAleatorio();
  }
  public abstract Comparable crearPorTeclado();
  public static Comparable crearPorTeclado(int opcion){
     FabricaDeComparables fabrica;
        switch (opcion){
      case numero:{
        fabrica=new FabricaDeNumeros();
        break;
      }
      case alumno:{
        fabrica= new FabricaDeAlumnos();
        break;
      }
      default:{

       fabrica= new FabricaDeVendedores();break;
      }
    }
    return fabrica.crearPorTeclado();
  }
      public static Comparable CrearComparablePorDefecto()
    {
      return crearAleatorio(fabricaPorDefecto);
    }
  }
  // actividad 5 y 9 tp3
  public class FabricaDeVendedores : FabricaDeComparables
  {
    public override Comparable crearPorTeclado()
    {
    Console.WriteLine("Nombre:");
    string nombreTeclado=Console.ReadLine();
    Console.WriteLine("dni:");
    int dniTeclado=(int)LectordeDatos.numeroPorTeclado();
    Console.WriteLine("sueldoBasico");
    double sueldoBasicoTeclado =(double)LectordeDatos.numeroPorTeclado();
    return new Vendedor(nombreTeclado, dniTeclado, sueldoBasicoTeclado);
    }
    public override Comparable crearAleatorio()
    {
      return new Vendedor(GeneradorDeDatosAleatorios.stringAleatorio(10),GeneradorDeDatosAleatorios.numerosAleatorios(1000000),(double)GeneradorDeDatosAleatorios.numerosAleatorios(100000));
    }
  }
  public class FabricaDeAlumnos : FabricaDeComparables
  {
   
    public override Comparable crearPorTeclado()
    {
    Console.WriteLine("Nombre:");
    string nombreTeclado=Console.ReadLine();
    Console.WriteLine("dni:");
    int dniTeclado=(int)LectordeDatos.numeroPorTeclado();
    Console.WriteLine("legajo:");
    int legajoTeclado =(int)LectordeDatos.numeroPorTeclado();
    Console.WriteLine("promedio:");
    float promedioTeclado = LectordeDatos.numeroPorTeclado();
    return new Alumno(nombreTeclado, dniTeclado, legajoTeclado, promedioTeclado);
    }
    public override Comparable crearAleatorio()
    {
      return new Alumno(GeneradorDeDatosAleatorios.stringAleatorio(10),GeneradorDeDatosAleatorios.numerosAleatorios(1000000),GeneradorDeDatosAleatorios.numerosAleatorios(1000000),(float)GeneradorDeDatosAleatorios.numerosAleatorios(1001)/100);
    }
  }
  public class FabricaDeNumeros : FabricaDeComparables{
    public override Comparable crearPorTeclado()
    {
    Console.WriteLine("numero:");
    return new Numero((int)LectordeDatos.numeroPorTeclado());
    }
    public override Comparable crearAleatorio(){
      return new Numero(GeneradorDeDatosAleatorios.numerosAleatorios(10000));
    }
  }
  // actividad 12 tp3
  public interface IObservado
  {
    void agregarObservador(IObservador o);
    void quitarObservador (IObservador o);
    void notificar();
  }
  public interface IObservador
  {
    void actualizar(IObservado ob);
  }
  //actividad 8 tp3
  public class Vendedor : Persona, IObservado
  {
    private double sueldoBasico;
    public double SueldoBasico{
      get{return sueldoBasico;}
      set{sueldoBasico=value;}
    }
    private double monto;
    public double getMonto()
    {return monto;}
    private List<IObservador> observadores= new List<IObservador>();
    private double bonus;
    public Vendedor (string n, int d, double s) : base (n,d)
    {
      sueldoBasico=s;
      bonus=1;
    }
    public void venta (double monto)
    {
      Console.WriteLine(monto);
      this.monto=monto;
      notificar();
    }
    public void aumentaBonus()
    {
      bonus+=0.1;
    }
    public override string ToString() // el metodo ToString existe en un metodo de object que aplica a todos los objetos y por eso esnecesario sobreescribirlo
    {
      return "DNI: " + dni.ToString() + ", Nombre: " + nombre.ToString() + ", Sueldo Basico: " + sueldoBasico.ToString()+", bonus acumulado:"+bonus.ToString();
    }
    public override bool sosIgual(Comparable c)
    {
      return this.sueldoBasico==((Vendedor)c).SueldoBasico;
    }
    public override bool sosMayor(Comparable c)
    {
      return this.sueldoBasico>((Vendedor)c).SueldoBasico;
    }
    public override bool sosMenor(Comparable c)
    {
      return this.sueldoBasico<((Vendedor)c).SueldoBasico;
    }
    public void agregarObservador(IObservador o)
    {
      observadores.Add(o);
    }
    public void quitarObservador(IObservador o)
    {
      observadores.Remove(o);
    }
    public void notificar()
    {
      foreach (IObservador o in observadores)
      {
        o.actualizar(this);
      }
    }

  }
  
    //actividad 11 tp3
    public class Gerente : IObservador{
      private List < Vendedor > mejores;
      public List<Vendedor> getVendedores()
      {
        return mejores;
      }
      public void cerrar ()
      {
        for (int i=0;i<mejores.Count;i++){
          Console.WriteLine(mejores[i]);
        }
      }
      public void venta (double monto, Vendedor vend)
      {
        if (monto>5000)
        {
          vend.aumentaBonus();
          if (!mejores.Contains(vend))
          {
            mejores.Add(vend);
          }
        }
      }
      public Gerente ()
      {
        mejores= new List<Vendedor>();
      }
      public void actualizar(IObservado ob)
      {
        venta(((Vendedor)ob).getMonto(), (Vendedor)ob);
      }
      
    }

  class Program {
    public static Random rnd = new Random();
    public static int rndRandom(int inicio, int fin) {
      return Program.rnd.Next(inicio, fin);
    }
    public static string nombreAleatorio() {
      int longitud = rnd.Next(4, 11);
      string nombre = "";
      for (int i = 0; longitud > i; i++) {
        nombre += (char) rnd.Next(97, 123);
      }
      return nombre;
    }
    // actividad 6 tp3
    public static void llenar(Coleccionable d, int opcion) {
      for (int i = 0; i < 20; i++) {
       Comparable elemento=FabricaDeComparables.crearAleatorio(opcion);
      d.agregar(elemento);
      }
    }
    public static void informar(Coleccionable d, int opcion) {
      Console.WriteLine(d.cuantos());
      Console.WriteLine(d.minimo());
      Console.WriteLine(d.maximo());
      Comparable comp=FabricaDeComparables.crearPorTeclado(opcion);
      if (d.contiene(comp)) {
        Console.WriteLine("El elemento leído está en la colección");
      } else {
        Console.WriteLine("El elemento leído no está en la colección");
      }

    }
    public static void llenarPersonas(Coleccionable c) {
      for (int i = 0; i < 20; i++) {
        Comparable personaAzar = new Persona(nombreAleatorio(), rnd.Next(10000000, 45000000));
        c.agregar(personaAzar);
      }
    }
    //actividad 2
    public static void llenarAlumnos(Coleccionable c) {
      for (int i = 0; i < 20; i++) {
        Comparable alumnoAzar = new Alumno(nombreAleatorio(), rnd.Next(10000000, 45000000), rnd.Next(1, 1000000), (float)(rnd.Next(0, 1001) / 100.0)); //cambiar!!!
        // actividad 2 tp2
        ((Alumno) alumnoAzar).setEstrategia(new StrategyAlumnosPorLegajo());
        c.agregar(alumnoAzar);
      }
    }
    // actividad 7 tp2
    public static void imprimirElementos(Coleccionable colec) {
      Iterador ite = ((Iterable) colec).getIterador();
      ite.primero();
      while (!ite.fin()) {
        Console.WriteLine(ite.actual());
        ite.siguiente();
      }
    }
    public static void cambiarEstrategia(Coleccionable c, StrategyAlumnos estrategia) {
      Iterador ite = ((Iterable) c).getIterador();
      ite.primero();
      while (!ite.fin()) {
        ((Alumno) ite.actual()).setEstrategia(estrategia);
        ite.siguiente();
      }
    }
    public static int charToIntñ(char a) {
      int retorno;
      a = char.ToLower(a);
      retorno = (int) a;
      if (a == 'ñ')
        return 110;
      if (retorno > 110 && retorno < 123)
        return retorno++;
      return retorno;
    }
    public static string ordenAlfa(string palabra1, string palabra2) {
      int longitudMin;
      string palabraCorta;
      if (palabra1.Length <= palabra2.Length) {
        longitudMin = palabra1.Length;
        palabraCorta = palabra1;
      } else {
        longitudMin = palabra2.Length;
        palabraCorta = palabra2;
      }
      for (int i = 0; i < longitudMin; i++) {
        if (charToIntñ(palabra1[i]) < charToIntñ(palabra2[i]))
          return palabra1;
        if (charToIntñ(palabra1[i]) > charToIntñ(palabra2[i]))
          return palabra2;
      }
      return palabraCorta;
    }
    // ejercicio 13 tp3
    public static void jornadaDeVentas (Iterable vendedores)
    {
      Iterador ite=vendedores.getIterador();
      for (int i=0;i<20;i++){
        ite.primero();
        while (!ite.fin())
        {
          double monto= GeneradorDeDatosAleatorios.numerosAleatorios(6999)+1;
          ((Vendedor)ite.actual()).venta(monto);
          ite.siguiente();
        }
      }
    }
    static void Main(string[] args) {
      /*// test actividad 2
      Comparable n1,n2,n3,n4;
      n1= new Numero(5);
      n2= new Numero(5);
      n3= new Numero(8);
      n4= new Numero(3);
      Console.WriteLine(n1.sosIgual(n2));
      Console.WriteLine(n1.sosMenor(n3));
      Console.WriteLine(n1.sosMayor(n4));
      //test activiadad 4
      Coleccionable col= new Pila();
      col.agregar(n1);
      col.agregar(n2);
      col.agregar(n3);
      Console.WriteLine(col.minimo()); // aparentemente la funcion WriteLine aplica el metodo Tostring a cualquier objeto que tome como parametro y por eso no es necesario hacer explicito que aplique el Tostring que yo implemente con override en la clase Numero
      Console.WriteLine(col.maximo());
      Console.WriteLine(col.contiene(n2));
      // test actividad 6
      informar(col);*/
      //actividad 7
      /*
      Pila pila1 = new Pila();
      Cola cola1 = new Cola();
      llenar(pila1);
      llenar(cola1);
      informar(pila1);
      informar(cola1);
      */
      /*
      // actividad 9
      Pila pila2= new Pila();
      Cola cola2= new Cola();
      ColeccionMultiple multiple = new ColeccionMultiple(pila2, cola2);//evidentemente esto pasa la posicion de memoria de las dos varias pq si en un futuro las modifico tambien se modifican en multiple
      llenar(pila2);
      llenar(cola2);
      informar(pila2);
      informar(cola2);
      informar(multiple);
      // activida 10. No modifique nada
      */
      /*
      // actividad 13
      Pila pila3= new Pila();
      Cola cola3= new Cola();
      ColeccionMultiple multiple2= new ColeccionMultiple(pila3, cola3);
      llenarPersonas(pila3);
      llenarPersonas(cola3);
      informar(multiple2);
      */
      // actividad 17
      // actividad 2 tp2
      /*
      Pila pila4 = new Pila();
      Cola cola4 = new Cola();
      ColeccionMultiple multiple4 = new ColeccionMultiple(pila4, cola4);
      llenarAlumnos(pila4);
      llenarAlumnos(cola4);
      informar(multiple4);
      */
      //testeo
      /*
      Conjunto conjunto1= new Conjunto();
      conjunto1.agregar(new Numero(5));
      conjunto1.agregar(new Numero(6));
      Console.WriteLine(conjunto1.contiene(new Numero(5)));
      Console.WriteLine(conjunto1.contiene(new Numero(4)));
      Pila pila4 = new Pila();
      Cola cola4 = new Cola();
      llenarAlumnos(pila4);
      llenarAlumnos(cola4);
       Iterador ite =cola4.getIterador();
      ite.primero();
      while (!ite.fin())
      {
          Console.WriteLine(ite.actual());
          ite.siguiente(); 
      }
      Console.WriteLine("");
      imprimirElementos(cola4);*/
      // actividad 8 tp2
      /*
      Pila pila = new Pila();
      Cola cola = new Cola();
      Conjunto conjunto = new Conjunto();
      Diccionario diccionario = new Diccionario();
      llenarAlumnos(pila);
      llenarAlumnos(cola);
      llenarAlumnos(conjunto);
      llenarAlumnos(diccionario);
      imprimirElementos(pila);
      Console.WriteLine("");
      imprimirElementos(cola);
      Console.WriteLine("");
      imprimirElementos(conjunto);
      Console.WriteLine("");
      imprimirElementos(diccionario);
      */
      // testeo actividad 9 tp2
      /*
      Cola cola8 = new Cola();
      llenarAlumnos(cola8);
      cambiarEstrategia(cola8, new StrategyAlumnosPorNombre());
      informar(cola8);*/

      // actividad 10 tp2
      /*
      Pila pila8 = new Pila();
      llenarAlumnos(pila8);
      cambiarEstrategia(pila8, new StrategyAlumnosPorNombre());
      informar(pila8);
      cambiarEstrategia(pila8, new StrategyAlumnosPorLegajo());
      informar(pila8);
      cambiarEstrategia(pila8, new StrategyAlumnosPorPromedio());
      informar(pila8);
      cambiarEstrategia(pila8, new StrategyAlumnosPorDNI());
      informar(pila8);
      */

      //test actividad 6 tp3
      /*
      Cola cola10= new Cola();
      llenar(cola10, 1);
      imprimirElementos(cola10);
      informar(cola10,1);
      */
      // actividad 6 tp3 (actividaddes 9 y 17 del tp1 modificado)
      /*
      Pila pila2= new Pila();
      Cola cola2= new Cola();
      ColeccionMultiple multiple = new ColeccionMultiple(pila2, cola2);//evidentemente esto pasa la posicion de memoria de las dos varias pq si en un futuro las modifico tambien se modifican en multiple
      llenar(pila2,0);
      llenar(cola2,0);
      informar(pila2,0);
      informar(cola2,0);
      informar(multiple,0);

      Pila pila4 = new Pila();
      Cola cola4 = new Cola();
      ColeccionMultiple multiple4 = new ColeccionMultiple(pila4, cola4);
      llenar(pila4,0);
      llenar(cola4,0);
      informar(multiple4,0);
      */
      /* //testeo ej12
      Vendedor v1= new Vendedor("a",1,20);
      Vendedor v2= new Vendedor("b",1,20);
      Vendedor v3= new Vendedor("c",1,20);
      Vendedor v4= new Vendedor("d",1,20);
      Gerente g5= new Gerente();

      v1.agregarObservador(g5);
      v2.agregarObservador(g5);
      v3.agregarObservador(g5);
      v4.agregarObservador(g5);
      v1.venta(4000);
      v2.venta(5500);
      v2.venta(5700);
      v3.venta(5000);
      v2.venta(5700);
      */
      

      // ejercicio 14 tp3
      Cola cola11= new Cola();
      llenar(cola11,0);
      Gerente g1=new Gerente();
      Iterador iteCola11=cola11.getIterador();
      iteCola11.primero();
      while (!iteCola11.fin())
      {
        ((Vendedor)iteCola11.actual()).agregarObservador(g1);
        iteCola11.siguiente();
      }
      jornadaDeVentas(cola11);
      g1.cerrar();

      Console.ReadKey();
    }
  }
}