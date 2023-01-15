import {NavLink} from "react-router-dom";
import {movieHomeStyles} from "./styles";
 
export const Home = () => {
    const styles = movieHomeStyles();
    return (
        <div className={styles.screen}>
            <div>
                <a href="https://vitejs.dev" target="_blank">
                    <img src="/vite.svg" className="logo" alt="Vite logo" />
                </a>
                <nav>
                    <NavLink to={"/movies"} className={styles.navLink}>Click to see Movies lisk</NavLink>
                </nav>
            </div>
        </div>
    )
}