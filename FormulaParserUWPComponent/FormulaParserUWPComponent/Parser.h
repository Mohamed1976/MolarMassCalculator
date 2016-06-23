#ifndef PARSER_H
#define PARSER_H 

/***********************************************/
/*                                             */
/*    Structures                               */
/*                                             */
/***********************************************/

typedef struct atom_count_struct *Atom_count_ptr;

typedef struct atom_count_struct {
	char *element_symbol;
	int count;
	Atom_count_ptr next;
} Atom_count;

typedef struct symbol_table_struct *Symtab_ptr;

typedef struct symbol_table_struct {
	Atom_count *start;
	Symtab_ptr next;
} Symtab;

typedef struct token_struct {
	/* 0 - left parenthesis
	1 - element name
	2 - int count number
	3 - right parenthesis
	*/
	int type;
	char *element_symbol;
	int count;
} Token;

typedef struct stack_struct *Stack_ptr;

typedef struct stack_struct {
	Symtab *first_tab;
	Symtab *last_tab;
	Stack_ptr prev;
} Stack;

/***********************************************/
/*                                             */
/*      Functions declarations                 */
/*                                             */
/***********************************************/

int verify_brackets(char *);
int check_brackets(char *, char *);
//int is_bracket(char);
int is_left_bracket(char);
char other_bracket(char);
int only_alnum(char *, char *);
int not_even(char *, char *);
char *matching_bracket(char *, char *);

Atom_count *parse_formula_c(char *formula);
void print_atom_count(Atom_count *i);
Atom_count *flatten(Symtab *n);
Atom_count *combine(Atom_count *n);
Atom_count *add_atom(Atom_count *i, Atom_count *j);
void free_symtab(Symtab *n);
int tokenize(Token *t, int *error, char **f);
char *make_str_copy(char *s);
void multiply(Atom_count *i, int n);
Atom_count *new_element(char *element_symbol);
Symtab *new_symtab(void);

#endif /* PARSER_H */