import {makeStyles, shorthands} from "@fluentui/react-components";

export const movieListStyles = makeStyles({
    screen: {
        width: '100%',
        display: 'flex',
        ...shorthands.padding('1px'),
        flexDirection: "column"
    },
    pagination: {
        display: "flex",
        "list-style": "none",
        columnGap: "1rem",
        "padding-inline-start": "0",
    },
    page: {},
    linkActive: {
        backgroundColor: "#ffffff",
        color: '#292929'
    },
    link: {
        cursor: "pointer",
        userSelect: "none",
        ...shorthands.border('1px', 'solid', '#a8a8a8'),
        ...shorthands.borderRadius('2px'),
        ...shorthands.padding("0.25rem", "0.5rem", "0.25rem", "0.5rem"),
    },
    disabledPageBtn:{
        cursor: "not-allowed"
    }
});
