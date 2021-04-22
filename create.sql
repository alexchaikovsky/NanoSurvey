-- Table: public.Survey

-- DROP TABLE public."Survey";

CREATE TABLE public."Survey"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    description text COLLATE pg_catalog."default",
    CONSTRAINT "Survey_pkey" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public."Survey"
    OWNER to test;
-- Table: public.Question

-- DROP TABLE public."Question";

CREATE TABLE public."Question"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    question_text text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Question_pkey" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public."Question"
    OWNER to test;


-- Table: public.Answer

-- DROP TABLE public."Answer";

CREATE TABLE public."Answer"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    answer_text text COLLATE pg_catalog."default" NOT NULL,
    question_id integer NOT NULL,
    CONSTRAINT "Answer_pkey" PRIMARY KEY (id),
    CONSTRAINT question_id FOREIGN KEY (question_id)
        REFERENCES public."Question" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public."Answer"
    OWNER to test;
-- Index: question_id_index

-- DROP INDEX public.question_id_index;

CREATE INDEX question_id_index
    ON public."Answer" USING btree
    (question_id ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.SurveyQuestions

-- DROP TABLE public."SurveyQuestions";

CREATE TABLE public."SurveyQuestions"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    survey_id integer NOT NULL,
    question_id integer NOT NULL,
    CONSTRAINT "Survey_Question_pkey" PRIMARY KEY (id),
    CONSTRAINT question_id FOREIGN KEY (question_id)
        REFERENCES public."Question" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT survey_id FOREIGN KEY (survey_id)
        REFERENCES public."Survey" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public."SurveyQuestions"
    OWNER to test;
-- Index: survey_id_index

-- DROP INDEX public.survey_id_index;

CREATE INDEX survey_id_index
    ON public."SurveyQuestions" USING btree
    (survey_id ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.Interview

-- DROP TABLE public."Interview";

CREATE TABLE public."Interview"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    survey_id integer NOT NULL,
    user_info text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Interview_pkey" PRIMARY KEY (id),
    CONSTRAINT survey_id FOREIGN KEY (survey_id)
        REFERENCES public."Survey" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public."Interview"
    OWNER to test;

-- Table: public.Result

-- DROP TABLE public."Result";

CREATE TABLE public."Result"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    answer_id integer NOT NULL,
    question_id integer NOT NULL,
    interview_id integer NOT NULL,
    CONSTRAINT "Result_pkey" PRIMARY KEY (id),
    CONSTRAINT answer_id FOREIGN KEY (answer_id)
        REFERENCES public."Answer" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT interview_id FOREIGN KEY (interview_id)
        REFERENCES public."Interview" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT question_id FOREIGN KEY (question_id)
        REFERENCES public."Question" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public."Result"
    OWNER to test;