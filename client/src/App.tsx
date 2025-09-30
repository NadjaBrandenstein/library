import './CSS/App.css'
import {createBrowserRouter, RouterProvider} from "react-router";
import Home from "./Home.tsx";
import Layout from "./Layout.tsx";

function App() {
    const router = createBrowserRouter([
        {
            path: "/",
            element: <Layout/>, //Layout wraps all pages
            children: [
                {path: "/", element: <Home/>}
                // {path: "/books/:bookId", element: <BookDetails/>}
                // {path: "/newbook", element: <NewBook/>}
                // {path: "/updatebook", element: <UpdateBook/>}
            ],
        },
    ]);

  return <RouterProvider router={router}/>
}

export default App
