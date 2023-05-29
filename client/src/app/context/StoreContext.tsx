import { PropsWithChildren, createContext, useContext, useState } from "react";
import { Basket } from "../models/basket";

// here we'll specify the different items and methods that we'll support in our StoreContext
interface StoreContextValue {
  basket: Basket | null;
  setBasket: (basket: Basket) => void;
  removeItem: (productId: number, quantity: number) => void;
};

// creamos el context con la interface
export const StoreContext = createContext<StoreContextValue | undefined>(undefined);

// lo consumimos (esto en el componente q necesita el estado)
export const useStoreContext = () => {
  const context = useContext(StoreContext)
  if(context === undefined){
    throw Error('Oops - we do not seem to be inside the provider');
  }
  return context;
}

// definimos lo de la interfaz y lo proveemos (esto lo usamos en el index.tsx)
export const StoreProvider = ({children}: PropsWithChildren<any>) => {
  const [basket, setBasket] = useState<Basket | null>(null);

  const removeItem = (productId: number, quantity: number) => {
    if(!basket) return;
    const items = [...basket.items];
    const itemIndex = items.findIndex(i => i.productId === productId)
    if(itemIndex >= 0){
      items[itemIndex].quantity -= quantity;
      if(items[itemIndex].quantity === 0) items.splice(itemIndex, 1);
      setBasket(prevState => {
        return {...prevState!, items}
      })
    }
  }

  return (
    <StoreContext.Provider value={{basket, setBasket, removeItem}}>
      {children}
    </StoreContext.Provider>
  )
}