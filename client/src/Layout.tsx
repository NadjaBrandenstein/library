import {useState} from "react";
import {Outlet, useNavigate} from "react-router";


export default function Home(){
    const [menuOpen, setMenuOpen] = useState(false);
    const navigate = useNavigate();

    return (
        <div>
            <div className="navbar">
                <div className={"relative"}>
                    <button className="button" onClick={() => setMenuOpen(!menuOpen)}>
                        ☰
                    </button>
                    {menuOpen && (
                        <div className="menu-dropdown">
                            <div onClick={() => navigate("/")}>Books</div>
                            <div onClick={() => navigate("/newbook")}>New Book</div>
                        </div>
                    )}
                </div>
                <h2 className="navbar-text">Library</h2>
            </div>
            <main>
                <Outlet/>
            </main>
        </div>
    )
}