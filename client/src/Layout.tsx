import {useState, useRef, useEffect} from "react";
import {Outlet, useNavigate} from "react-router";


export default function Home(){
    const [menuOpen, setMenuOpen] = useState(false);
    const menuRef = useRef<HTMLDivElement>(null);
    const navigate = useNavigate();

    useEffect(() => {
        function handleClickOutside(event: MouseEvent) {
            if(menuRef.current && !menuRef.current.contains(event.target as Node)){
                setMenuOpen(false);
            }
        }
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    },[])

    return (
        <div>
            <div className="navbar">
                <div className="menu-container" ref={menuRef}>
                    <button className="button" onClick={() => setMenuOpen(!menuOpen)}>
                        ☰
                    </button>
                    {menuOpen && (
                        <div className="menu-dropdown">
                            <div onClick={() => {navigate("/"); setMenuOpen(false)}}>Books</div>
                            {/*<div onClick={() => {navigate("/newbook"); setMenuOpen(false)}}>Add Book</div>*/}
                            {/*<div onClick={() => {navigate("/updatebook"); setMenuOpen(false)}}>Update Book</div>*/}
                        </div>
                    )}
                </div>
                <h2 className="navbar-text">Library</h2>
            </div>
            <main className="main-content">
                <Outlet/>
            </main>
        </div>
    )
}