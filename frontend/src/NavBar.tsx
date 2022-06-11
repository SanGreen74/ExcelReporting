import { Link, useMatch, useResolvedPath } from "react-router-dom";

export default function Navbar(): JSX.Element {
  return (
    <nav className="nav">
      <ul>
        <CustomLink to="/pko">Формирование ПКО</CustomLink>
      </ul>
    </nav>
  );
}

interface CustomLinkProps {
  to: string;
  children: any;
}

function CustomLink({ to, children, ...props }: CustomLinkProps) {
  const resolvedPath = useResolvedPath(to);
  const isActive = useMatch({ path: resolvedPath.pathname, end: true });

  return (
    <li className={isActive ? "active" : ""}>
      <Link to={to} {...props}>
        {children}
      </Link>
    </li>
  );
}
