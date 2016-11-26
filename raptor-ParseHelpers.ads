-- Autogenerated by MSIL2Ada v. 2
-- By: Martin C. Carlisle
--     Department of Computer Science
--     US Air Force Academy
--     carlislem@acm.org
with MSSyst.Object;
with MSIL_Types;
use MSIL_Types;
limited with MSSyst.String;
limited with MSSyst.Type_k;
with numbers;
package raptor.ParseHelpers is
   type Typ is new MSSyst.Object.Typ   with record
      null;
   end record;
   type Ref is access all Typ'Class;
   type Ref_addrof is access all Ref;
   type Ref_Arr is array(Natural range <>) of Ref;
   type Ref_Array is access all Ref_Arr;
   type Ref_Array_addrof is access all Ref_Array;
   function new_ParseHelpers(
      This : Ref := null) return Ref;
   function addExpression(
      o : access MSSyst.Object.Typ'Class;
      e : access MSSyst.Object.Typ'Class) return Integer;
   procedure addValue(
      o : access MSSyst.Object.Typ'Class;
      v : numbers.value);
   procedure clearExpressions(
      o : access MSSyst.Object.Typ'Class);
   function getValue(
      o : access MSSyst.Object.Typ'Class;
      i : Integer) return numbers.value;
private
   pragma Convention(MSIL,Typ);
   pragma MSIL_Constructor(new_ParseHelpers);
   pragma Import(MSIL,addExpression,"addExpression");
   pragma Import(MSIL,addValue,"addValue");
   pragma Import(MSIL,clearExpressions,"clearExpressions");
   pragma Import(MSIL,getValue,"getValue");
end raptor.ParseHelpers;
pragma Import(MSIL,raptor.ParseHelpers,
   ".ver 4:0:5:2",
   "[raptor]raptor.ParseHelpers");
