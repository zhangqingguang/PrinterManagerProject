U S E   [ m a s t e r ]  
 G O  
 / * * * * * *   O b j e c t :     D a t a b a s e   [ P r i n t e r M a n a g e r D B ]         S c r i p t   D a t e :   1 2 / 1 5 / 2 0 1 8   1 4 : 0 5 : 0 0   * * * * * * /  
 C R E A T E   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   O N     P R I M A R Y    
 (   N A M E   =   N ' P r i n t e r M a n a g e r D B ' ,   F I L E N A M E   =   N ' C : \ P r i n t e r M a n a g e r D B . m d f '   ,   S I Z E   =   3 0 7 2 K B   ,   M A X S I Z E   =   U N L I M I T E D ,   F I L E G R O W T H   =   1 0 2 4 K B   )  
   L O G   O N    
 (   N A M E   =   N ' P r i n t e r M a n a g e r D B _ l o g ' ,   F I L E N A M E   =   N ' C : \ P r i n t e r M a n a g e r D B _ l o g . l d f '   ,   S I Z E   =   1 0 2 4 K B   ,   M A X S I Z E   =   2 0 4 8 G B   ,   F I L E G R O W T H   =   1 0 % )  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   C O M P A T I B I L I T Y _ L E V E L   =   1 0 0  
 G O  
 I F   ( 1   =   F U L L T E X T S E R V I C E P R O P E R T Y ( ' I s F u l l T e x t I n s t a l l e d ' ) )  
 b e g i n  
 E X E C   [ P r i n t e r M a n a g e r D B ] . [ d b o ] . [ s p _ f u l l t e x t _ d a t a b a s e ]   @ a c t i o n   =   ' e n a b l e '  
 e n d  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A N S I _ N U L L _ D E F A U L T   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A N S I _ N U L L S   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A N S I _ P A D D I N G   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A N S I _ W A R N I N G S   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A R I T H A B O R T   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A U T O _ C L O S E   O N  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A U T O _ C R E A T E _ S T A T I S T I C S   O N  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A U T O _ S H R I N K   O N  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A U T O _ U P D A T E _ S T A T I S T I C S   O N  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   C U R S O R _ C L O S E _ O N _ C O M M I T   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   C U R S O R _ D E F A U L T     G L O B A L  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   C O N C A T _ N U L L _ Y I E L D S _ N U L L   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   N U M E R I C _ R O U N D A B O R T   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   Q U O T E D _ I D E N T I F I E R   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   R E C U R S I V E _ T R I G G E R S   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T     D I S A B L E _ B R O K E R  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A U T O _ U P D A T E _ S T A T I S T I C S _ A S Y N C   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   D A T E _ C O R R E L A T I O N _ O P T I M I Z A T I O N   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   T R U S T W O R T H Y   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   A L L O W _ S N A P S H O T _ I S O L A T I O N   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   P A R A M E T E R I Z A T I O N   S I M P L E  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   R E A D _ C O M M I T T E D _ S N A P S H O T   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   H O N O R _ B R O K E R _ P R I O R I T Y   O F F  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T     R E A D _ W R I T E  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   R E C O V E R Y   F U L L  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T     M U L T I _ U S E R  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   P A G E _ V E R I F Y   C H E C K S U M  
 G O  
 A L T E R   D A T A B A S E   [ P r i n t e r M a n a g e r D B ]   S E T   D B _ C H A I N I N G   O F F  
 G O  
 E X E C   s y s . s p _ d b _ v a r d e c i m a l _ s t o r a g e _ f o r m a t   N ' P r i n t e r M a n a g e r D B ' ,   N ' O N '  
 G O  
 U S E   [ P r i n t e r M a n a g e r D B ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ v _ u s e r s ]         S c r i p t   D a t e :   1 2 / 1 5 / 2 0 1 8   1 4 : 0 5 : 0 1   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ v _ u s e r s ] (  
 	 [ I D ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ u s e r _ n a m e ]   [ n v a r c h a r ] ( 5 0 )   N O T   N U L L ,  
 	 [ p a s s w o r d ]   [ v a r c h a r ] ( 1 0 0 )   N O T   N U L L ,  
 	 [ c r e a t o r ]   [ i n t ]   N U L L ,  
 	 [ c r e a t e t i m e ]   [ d a t e t i m e ]   N O T   N U L L ,  
   C O N S T R A I N T   [ P K _ v _ u s e r s ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ I D ]   A S C  
 ) W I T H   ( P A D _ I N D E X     =   O F F ,   S T A T I S T I C S _ N O R E C O M P U T E     =   O F F ,   I G N O R E _ D U P _ K E Y   =   O F F ,   A L L O W _ R O W _ L O C K S     =   O N ,   A L L O W _ P A G E _ L O C K S     =   O N )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ v _ o r d e r _ i n f o ]         S c r i p t   D a t e :   1 2 / 1 5 / 2 0 1 8   1 4 : 0 5 : 0 1   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ v _ o r d e r _ i n f o ] (  
 	 [ I D ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ d r u g _ i d ]   [ b i g i n t ]   N U L L ,  
 	 [ d r u g _ n u m b e r ]   [ v a r c h a r ] ( 2 0 )   N U L L ,  
 	 [ d r u g _ w e i g h t ]   [ i n t ]   N U L L ,  
 	 [ d r u g _ n a m e ]   [ n v a r c h a r ] ( 1 0 0 )   N U L L ,  
 	 [ d r u g _ s p e c ]   [ n v a r c h a r ] ( 4 0 )   N U L L ,  
 	 [ u s a g e _ i d ]   [ v a r c h a r ] ( 1 0 )   N U L L ,  
 	 [ u s e _ o r g ]   [ n v a r c h a r ] ( 2 0 )   N U L L ,  
 	 [ u s e _ c o u n t ]   [ i n t ]   N U L L ,  
 	 [ u s e _ u n i t ]   [ n v a r c h a r ] ( 1 0 )   N U L L ,  
 	 [ u s e _ f r e q u e n c y ]   [ n v a r c h a r ] ( 3 0 )   N U L L ,  
 	 [ s t a r t _ d a t e ]   [ v a r c h a r ] ( 1 0 )   N U L L ,  
 	 [ s t o p _ d a t e ]   [ v a r c h a r ] ( 1 0 )   N U L L ,  
 	 [ u s e _ t i m e ]   [ v a r c h a r ] ( 1 6 )   N U L L ,  
 	 [ o r d e r _ n u m b e r ]   [ v a r c h a r ] ( 2 0 )   N U L L ,  
 	 [ o r d e r _ r e m a r k ]   [ n v a r c h a r ] ( 2 0 0 )   N U L L ,  
 	 [ o r d e r _ t y p e ]   [ i n t ]   N U L L ,  
 	 [ p a s s _ r e m a r k ]   [ n v a r c h a r ] ( 2 0 0 )   N U L L ,  
 	 [ d o c t o r _ n a m e ]   [ n v a r c h a r ] ( 2 0 )   N U L L ,  
 	 [ p a t i e n t i d ]   [ v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ p a t i e n t _ n a m e ]   [ v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ p a t i e n t _ g e n d e r ]   [ v a r c h a r ] ( 2 )   N U L L ,  
 	 [ b a t c h ]   [ v a r c h a r ] ( 1 0 )   N U L L ,  
 	 [ d e p a r t m e n t _ n u m b e r ]   [ v a r c h a r ] ( 2 0 )   N U L L ,  
 	 [ d e p a r t m e n t _ n a m e ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ v i s i t i d ]   [ i n t ]   N U L L ,  
 	 [ g r o u p _ n u m ]   [ i n t ]   N U L L ,  
 	 [ s n _ n u m ]   [ i n t ]   N U L L ,  
 	 [ m l _ s p e e d ]   [ i n t ]   N U L L ,  
 	 [ c r e a t e _ d a t e ]   [ d a t e t i m e ]   N U L L ,  
 	 [ o r d e r _ s t a t u s ]   [ v a r c h a r ] ( 2 )   N U L L ,  
 	 [ i s _ t w i c e _ p r i n t ]   [ v a r c h a r ] ( 2 )   N U L L ,  
 	 [ c h e c k e r ]   [ n v a r c h a r ] ( 2 0 )   N U L L ,  
 	 [ d e l i v e r y e r ]   [ n v a r c h a r ] ( 2 0 )   N U L L ,  
 	 [ r e c h e c k e r ]   [ n v a r c h a r ] ( 2 0 )   N U L L ,  
 	 [ c o n f i g _ p e r s o n ]   [ n v a r c h a r ] ( 2 0 )   N U L L ,  
 	 [ c o n f i g _ d a t e ]   [ d a t e t i m e ]   N U L L ,  
 	 [ u s a g e _ n a m e ]   [ n v a r c h a r ] ( 5 0 )   N U L L ,  
 	 [ b e d _ n u m b e r ]   [ v a r c h a r ] ( 2 0 )   N U L L ,  
   C O N S T R A I N T   [ P K _ v _ o r d e r _ i n f o ]   P R I M A R Y   K E Y   C L U S T E R E D    
 (  
 	 [ I D ]   A S C  
 ) W I T H   ( P A D _ I N D E X     =   O F F ,   S T A T I S T I C S _ N O R E C O M P U T E     =   O F F ,   I G N O R E _ D U P _ K E Y   =   O F F ,   A L L O W _ R O W _ L O C K S     =   O N ,   A L L O W _ P A G E _ L O C K S     =   O N )   O N   [ P R I M A R Y ]  
 )   O N   [ P R I M A R Y ]  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' o��Ti d '   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' d r u g _ i d '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' o��T�S'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' d r u g _ n u m b e r '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' o��TCg͑'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' d r u g _ w e i g h t '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' o��TT�y'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' d r u g _ n a m e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' o��Tĉ<h'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' d r u g _ s p e c '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' (u�li d '   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' u s a g e _ i d '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' O(uUSMO'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' u s e _ o r g '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' O(upeϑ'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' u s e _ c o u n t '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' O(uUSMO'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' u s e _ u n i t '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' O(u��!k'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' u s e _ f r e q u e n c y '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N '  _�Y�e��'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' s t a r t _ d a t e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' �~_g�e��'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' s t o p _ d a t e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' O(u�e��'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' u s e _ t i m e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' ;S1V�S'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' o r d e r _ n u m b e r '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' ;S1VY�l'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' o r d e r _ r e m a r k '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' ;S1V{|�W'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' o r d e r _ t y p e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' Y�M�Y�l'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' p a s s _ r e m a r k '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' ;Su�YT'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' d o c t o r _ n a m e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' �`�i d '   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' p a t i e n t i d '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' �`��YT'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' p a t i e n t _ n a m e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' �`�'`+R'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' p a t i e n t _ g e n d e r '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' RRyb!k'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' b a t c h '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' ���S'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' d e p a r t m e n t _ n u m b e r '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' ��T�y'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' d e p a r t m e n t _ n a m e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' ,g!kOOb�hƋ'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' v i s i t i d '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' �~�S'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' g r o u p _ n u m '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' �^�S'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' s n _ n u m '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' �n�'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' m l _ s p e e d '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' R�^�e��'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' c r e a t e _ d a t e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' ;S1V�r`'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' o r d e r _ s t a t u s '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' /f&T͑Sb'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' i s _ t w i c e _ p r i n t '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' �[8h�N'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' c h e c k e r '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' Fdo��N'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' d e l i v e r y e r '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' Y8h�N'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' r e c h e c k e r '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' M�o��N'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' c o n f i g _ p e r s o n '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' M�n�e��'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' c o n f i g _ d a t e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' (u�l�f'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' u s a g e _ n a m e '  
 G O  
 E X E C   s y s . s p _ a d d e x t e n d e d p r o p e r t y   @ n a m e = N ' M S _ D e s c r i p t i o n ' ,   @ v a l u e = N ' �^MO�S'   ,   @ l e v e l 0 t y p e = N ' S C H E M A ' , @ l e v e l 0 n a m e = N ' d b o ' ,   @ l e v e l 1 t y p e = N ' T A B L E ' , @ l e v e l 1 n a m e = N ' v _ o r d e r _ i n f o ' ,   @ l e v e l 2 t y p e = N ' C O L U M N ' , @ l e v e l 2 n a m e = N ' b e d _ n u m b e r '  
 G O  
 