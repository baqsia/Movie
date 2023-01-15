import {makeStyles, shorthands} from "@fluentui/react-components";

export const movieDetailsStyles = makeStyles({
    screen: {
        width: '100%',
        display: 'flex',
        ...shorthands.padding('3rem'),
        flexDirection: "column"
    },
    details: {
        marginTop: "1.5rem",
        display: "flex",
        flexDirection: "column",
        ...shorthands.gap("rem"),
        "& div": {
            "& h3": {
                ...shorthands.margin("0.3rem")
            },
            "& h2": {
                ...shorthands.margin("0.3rem")
            }
        }
    }
});
