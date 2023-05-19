import { Navigate, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import Catalog from "../../features/catalog/Catalog";
import ProductDetails from "../../features/catalog/ProductDetails";
import AboutPage from "../../features/about/AboutPage";
import ContactPage from "../../features/contact/ContactPage";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";

export const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
      {path: '', element: <HomePage />},
      {path: 'catalog', element: <Catalog />},
      {path: 'catalog/:id', element: <ProductDetails />},
      {path: 'about', element: <AboutPage />},
      {path: 'contact', element: <ContactPage />},
      {path: 'server-error', element: <ServerError />},
      {path: 'not-found', element: <NotFound />},
      {path: '*', element: <Navigate replace to='not-found' />},   // (*)
    ]
  }
])

// la única diferencia entre: {path: '*', element: <NotFound />}, y {path: '*', element: <NotFound />}
// es q el 1ero te retorna el componente not-found pero con la url anterior (ej: localhost:3000/path-inexistente) y el 2do la replaza por localhost:3000/path-inexistente